#include <iostream>
#include "ListPointer.h"
using namespace std;

Animals* ListPointer::operator* ()// WORK
{
	return ptr->anml;
}

Animals* ListPointer::operator++ ()//pre, like ++X
{
	if (ptr->next!=NULL)
		ptr=ptr->next;
	return **this;// return the Animal
}

Animals* ListPointer::operator++ (int)// post, like X++
{
	if (ptr->next==NULL)
		return **this;
	else {
		ptr=ptr->next;
		return ptr->prev->anml;
	}
}

Animals* ListPointer::operator-- ()//pre, like --X
{
	if (ptr->prev!=NULL)
		ptr=ptr->prev;
	return **this;

}
Animals* ListPointer::operator-- (int)
{
	if (ptr->prev==NULL)
		return **this;
	else {
		ptr=ptr->prev;
		return ptr->next->anml;
	}
}