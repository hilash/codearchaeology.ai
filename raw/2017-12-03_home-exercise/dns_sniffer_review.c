/**************************************************
DNS response sniffer, By Hila Shmuel


Implemented here only the UDP version, since it's
way more common.
This code is not extra pretty, but it's simple 
and get the job done.

notice there is no dns packet struct on linux
files, so had to write it myself.

DNS packet format based on
http://www.zytrax.com/books/dns/ch15/
and RFC 1035.
**************************************************/

/* CR: very well done, straight to the point and you were not afraid to get your hands dirty with the RFC.
  ( Dima: I think you have what it takes to be a dark embedded Linux ninja lord ). 
  ( Netanel: Nice way to filter packets is using BPF, then the kernel filters the packets for you, e.g port 53.)*/

#include <netinet/udp.h> 
#include <netinet/ip.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>
#include <unistd.h>

#define PACKET_MAX_LEN  (65536)
#define DNS_TYPE_A	(0x1)

/* CR: these structs are supposed to be packed, with __attribute__((packed)) - you relied on native x86 alignment to align on a 4-byte boundary.*/
/* CR: you used uint16_t without including inttypes.h */
struct dnshdr
{
	uint16_t id;		/* identification number */
	uint16_t flags;
	uint16_t qcount;	/* number of question entries */
	uint16_t ancount;	/* number of answer entries */
	uint16_t nscount;	/* number of authority entries */ 
	uint16_t adcount;	/* number of resource entries */
};

struct dnshdr_question
{

	uint16_t qtype;
	uint16_t qclass;
};

struct dnshdr_answer
{
	uint16_t type;
	uint16_t class;
	uint32_t TTL;
	uint16_t rdlength;
	uint16_t rdata;		/* array with size of rdlength */
};


/*
RFC 1035: DNS Header section format

                                    1  1  1  1  1  1
      0  1  2  3  4  5  6  7  8  9  0  1  2  3  4  5
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |                      ID                       |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |QR|   Opcode  |AA|TC|RD|RA|   Z    |   RCODE   |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |                    QDCOUNT                    |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |                    ANCOUNT                    |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |                    NSCOUNT                    |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |                    ARCOUNT                    |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
*/

/* 
	Question section format

                            1  1  1  1  1  1
      0  1  2  3  4  5  6  7  8  9  0  1  2  3  4  5
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |                                               |
    /                     QNAME                     /
    /                                               /
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |                     QTYPE                     |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |                     QCLASS                    |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
*/

void parse_packet_and_print_dns_data(unsigned char * buffer , ssize_t data_size);
void parse_dns_question_section(unsigned char * entry_buffer , ssize_t * data_size);
void parse_dns_answer_section(unsigned char * entry_buffer , ssize_t * data_size, unsigned char * dns_data_buffer);
void parse_label(unsigned char * label_buffer, unsigned char * label, ssize_t * label_len);

int main()
{
	/* CR: generally speaking, initializing file descriptors with 0 is a bad habit because you risk close(0) and that kills your stdin. */
	int udp_socket=0;
 	ssize_t num_bytes_recieved;
	struct sockaddr saddr;
    	struct in_addr in;
	socklen_t saddr_size = sizeof(saddr);
	
	/* allocate pcket buffer */
    	unsigned char * buffer = (unsigned char *)malloc(PACKET_MAX_LEN);
	if (NULL==buffer){
		/* CR: real basa, Lol. */
		printf("Malloc failed.. Basssaaa\n");
		return 1;
	}

	/* create a raw socket and bind it to the public interface */
	udp_socket = socket(AF_INET , SOCK_RAW , IPPROTO_UDP);
	if(udp_socket < 0) {
	        perror("Socket Error\n");
	        return 1;
	}

	while(true) {	
		/* receive packet */
        	num_bytes_recieved = recvfrom(udp_socket, buffer, PACKET_MAX_LEN, 0, &saddr, &saddr_size);
        	if(num_bytes_recieved <0 ) {
            		perror("Recvfrom error\n");
					/* CR: leak here, valgrind --track-fds=yes is a true friend in these cases (close(udp_socket)).*/
					/* CR: valgrind would have failed, free(buffer) is missing. */
            		return 1;
        	}

        	/* parse the packet */
        	parse_packet_and_print_dns_data(buffer , num_bytes_recieved);
  	}
   	close(udp_socket);
	
	/* CR: valgrind would have failed, free(buffer) is missing. */

	return 0;
}




void parse_packet_and_print_dns_data(unsigned char * buffer , ssize_t data_size)
{
	/* IP Header */
	unsigned short ip_header_len;
	struct sockaddr_in source,dest;
	struct iphdr *ip_header = (struct iphdr *)buffer;
	ip_header_len = ip_header->ihl*4;
     
	/* CR: you want to work with struct in_addr here. */
    memset(&source, 0, sizeof(source));
	memset(&dest, 0, sizeof(dest));
	source.sin_addr.s_addr = ip_header->saddr;
	dest.sin_addr.s_addr   = ip_header->daddr;


	/* UDP Header */
	struct udphdr *udp_header = (struct udphdr*)(buffer + ip_header_len);
  

	/* CR: generally it's better to do htons(53) rather than flipping the udp_header->source because htons(53) consists
	   of arithmetics on a constant and the compiler pre-computes it, see byteswap.h, bswap_16(). */
	/* CR: potential crash when you dereference udp_header->source because of a missing offset check */
	/* DNS Header */
	/* DNS uses port 53. hence, for DNS query, the dst port is 53. for DNS response, the src port is 53 */
	if (ntohs(udp_header->source) != 53) {
		return;
	}
	
	/* Check dns header size ok */
	ssize_t dns_packet_size = data_size - ip_header_len - sizeof(struct udphdr); 
	if (dns_packet_size < 12) {
		printf("Error: src port is 53 (DNS), but size of data header (%lu) is smaller than the lenght of DNS header (12)\n", dns_packet_size);
		return;
	}
	/* CR: any <= 0 size is not OK */

	
	struct dnshdr *dns_header = (struct dnshdr*)(buffer + ip_header_len + sizeof(struct udphdr));
	unsigned char *dns_data = (unsigned char *)dns_header + sizeof(struct dnshdr); /* pointer to the current dns payload */

	/* Check the dns.flags.QR bit. Query - 0/Response - 1 */
	if (1 != (ntohs(dns_header->flags)) >> 15) {
		return;
	}

	/* if there is not type response, we are not intrested. we'll check later if it's an A response */
	if (ntohs(dns_header->ancount) < 1) {
		return;
	}

	printf("********************************************************************\n");
	printf("Source IP : %s\n", inet_ntoa(source.sin_addr));
	printf("Dest IP   : %s\n", inet_ntoa(dest.sin_addr));
	printf("Source Port : %d\n" , ntohs(udp_header->source));
    	printf("Dest   Port : %d\n" , ntohs(udp_header->dest));
	
	printf("DNS::    ID: 		%d\n", ntohs(dns_header->id));
	printf("         Flags: 	%x\n", ntohs(dns_header->flags));
	printf("         Num. Questions:%d\n", ntohs(dns_header->qcount));
	printf("         Num. Answers:  %d\n", ntohs(dns_header->ancount));
	printf("         Num. Authority:%d\n", ntohs(dns_header->nscount));
	printf("         Num. Resource: %d\n", ntohs(dns_header->adcount));

	/* go over all Questions entries, inorder to calculate where the answer section */
	for (int i=0; i < ntohs(dns_header->qcount); i++) {
		ssize_t entry_size = 0;
		printf("DNS::	QUESTION %d\n", i+1); 
		parse_dns_question_section(dns_data, &entry_size);

		dns_data += entry_size; /*advance the pointer to the next entry */
		/* CR: dns_header->qcount could be big while the packet you received has less (struct dnshdr_question)s in it */
	}

	/* go over all Answers entries - Answer of Type A has the ip of the wanted address */
	for (int i=0; i < ntohs(dns_header->ancount); i++) {
		ssize_t entry_size = 0;
		printf("DNS::	ANSWER %d\n", i+1); 
		parse_dns_answer_section(dns_data, &entry_size, (unsigned char *)dns_header);

		dns_data += entry_size; /*advance the pointer to the next entry */
	}
}


void parse_dns_question_section(unsigned char * entry_buffer , ssize_t * data_size)
{
/*QNAME:a domain name represented as a sequence of labels, where
        each label consists of a length octet followed by that
        number of octets.  The domain name terminates with the
        zero length octet for the null label of the root.  Note
        that this field may be an odd number of octets; no
        padding is used.*/
	/* CR: This could be static array instead of defining it on the stack each func call. */
	unsigned char label[1000];
	ssize_t label_len = 0;

	parse_label(entry_buffer, label, &label_len); 
	struct dnshdr_question *question_entry = (struct dnshdr_question*)(entry_buffer+label_len);

	printf("		QNNAME: %s\n", label);
	printf("		QTYPE:  %d\n", ntohs(question_entry->qtype));
	printf("		QCLASS: %d\n",  ntohs(question_entry->qclass));

	*data_size = label_len + sizeof(struct dnshdr_question);
}

/* parse DNS label.
	label_buffer - raw label buffer
	label - the nice formatted label, after parsing
	label length - size of raw label buffer, including null terminator */
/*QNAME:a domain name represented as a sequence of labels, where
        each label consists of a length octet followed by that
        number of octets.  The domain name terminates with the
        zero length octet for the null label of the root.  Note
        that this field may be an odd number of octets; no
        padding is used.*/
void parse_label(unsigned char * label_buffer, unsigned char * label, ssize_t * label_len) 
{
	int j = 0;
	int d = 0;
	unsigned char domain_name[1000];
	
	/* Parse labels */
	while (*(label_buffer+j) != 0) {
		unsigned char length = *(label_buffer+j);
		j++;

		for (int i=0; i < length; i++) {
			label[d]= *(label_buffer+j);
			j++;
			d++;
		}
		label[d] = '.';
		d++;
	}
	label[d-1] = 0x0;
	*label_len = j+1;
} 

/*
4.1.3. Resource record format

The answer, authority, and additional sections all share the same
format: a variable number of resource records, where the number of
records is specified in the corresponding count field in the header.
Each resource record has the following format:
                                    1  1  1  1  1  1
      0  1  2  3  4  5  6  7  8  9  0  1  2  3  4  5
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |                                               |
    /                                               /
    /                      NAME                     /
    |                                               |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |                      TYPE                     |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |                     CLASS                     |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |                      TTL                      |
    |                                               |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    |                   RDLENGTH                    |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--|
    /                     RDATA                     /
    /                                               /
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+


4.1.4. Message compression

In order to reduce the size of messages, the domain system utilizes a
compression scheme which eliminates the repetition of domain names in a
message.  In this scheme, an entire domain name or a list of labels at
the end of a domain name is replaced with a pointer to a prior occurance
of the same name.

The pointer takes the form of a two octet sequence:

    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
    | 1  1|                OFFSET                   |
    +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+

The first two bits are ones.  This allows a pointer to be distinguished
from a label, since the label must begin with two zero bits because
labels are restricted to 63 octets or less.  (The 10 and 01 combinations
are reserved for future use.)  The OFFSET field specifies an offset from
the start of the message (i.e., the first octet of the ID field in the
domain header).  A zero offset specifies the first byte of the ID field,
etc.

The compression scheme allows a domain name in a message to be
represented as either:

   - a sequence of labels ending in a zero octet

   - a pointer

   - a sequence of labels ending with a pointer

Pointers can only be used for occurances of a domain name where the
format is not class specific.  If this were not the case, a name server
or resolver would be required to know the format of all RRs it handled.
As yet, there are no such cases, but they may occur in future RDATA
formats.

*/

void parse_dns_answer_section(unsigned char * entry_buffer , ssize_t * data_size, unsigned char * dns_data_buffer)
{	
	unsigned char label[1000];
	ssize_t label_len = 0;
	uint16_t entry_offset=0;

	/* we can have POINTER (read the RFC above) or regular label. let's check! if the twe MSB == 11 (3) */
	uint16_t pointer = ntohs(*((uint16_t*)(entry_buffer)));
	if ((pointer >> 14) == 3){
		/* extract the pointer offset */
		uint16_t pointer_offset = 0xc000 ^ pointer;
		parse_label(dns_data_buffer + pointer_offset, label, &label_len); 
		entry_offset = 2;
	}
	else
	{
		parse_label(entry_buffer, label, &label_len); 
		entry_offset = label_len;
	}

	/* Note: Pointers, if used, terminate names. The name field may consist of :
		1. a label (or sequence of labels) terminated with a zero length record 
		2. OR a single pointer
		3. OR a label (or label sequence) terminated with a pointer. 

	TODO: i didn't implement the third option. didn't see any packets like this on wireshark, 
	and it's not really important (since it's not a real code and i have other stuff to do :) 
	*/

	struct dnshdr_answer *answer_entry = (struct dnshdr_answer*)(entry_buffer+entry_offset);

	printf("		NAME: %s\n", label);
	printf("		TYPE:  %d\n", ntohs(answer_entry->type));
	printf("		CLASS: %d\n",  ntohs(answer_entry->class));
	printf("		TTL: %d\n",  ntohl(answer_entry->TTL));
	printf("		RDLENGTH: %d\n",  ntohs(answer_entry->rdlength));

	/* CR: Yoda style is an army thing, it's less readable */
	if (DNS_TYPE_A == ntohs(answer_entry->type)) {	
		/* CR: you want a struct in_addr here */
		struct sockaddr_in ip;
    		memset(&ip, 0, sizeof(ip));
		ip.sin_addr.s_addr = answer_entry->rdata;
		printf("		ADDRESS: %s\n",  inet_ntoa(ip.sin_addr));
	} else {
		/* TODO: this data should be parsed according to the "TYPE" field */
		printf("		RDATA:   %d\n",  ntohs(answer_entry->rdata));
	}

	*data_size = entry_offset + sizeof(struct dnshdr_answer) + ntohs(answer_entry->rdlength) - 2;
}