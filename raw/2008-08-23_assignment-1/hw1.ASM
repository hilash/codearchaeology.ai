;HW1: a subroutine that prints a warning message if there's carry after execute ADD command.
;
;
;
;
		.MODEL LARGE
		PUBLIC	_start_track
		PUBLIC	_end_track
		EXTRN	_printf:FAR
;
		.DATA
Int1Save	DD ?
Was_ADD		DB 0
printf_str	DB 10,' Warning: Carry on ADD',10,0
;
		.CODE
_Single_Step_Handler PROC	FAR
;
;	if (Was_ADD==1), then the previous command was 'ADD'
;		check its flags - if there is carry - print a message.
;	pop ip & cs, check if the next command is 'ADD'
;		if so, then flag:=1.
;			else flag:=0.
;
		PUSH	BP				;preserve registers & segments
		MOV		BP,SP
		PUSH	AX
		PUSH	BX
		PUSH	CX
		PUSH	DX
		PUSH	ES
		PUSH	DS
		PUSH	SI
		PUSH	DI
						
		CMP		Was_ADD,1		;if Was_ADD == 1, check carry, else 
		JNE		CHECK_ADD
		
		PUSHF					;preserve flags register
		
		MOV		AX,[BP+6]		;ax=[bp+6]=flags register after executing ADD
		SHR		AX,1			;put the carry flag (was set after executing ADD) in the current carry flag	
		
		JNC		END_CARRY		;if there carry - print messege
		
		PUSH	DS				;print warning if there's carry after execute ADD
		MOV		AX,SEG printf_str	
		MOV		DS,AX
		PUSH	DS
		PUSH	OFFSET printf_str
		CALL	_printf
		ADD		SP,4
		POP		DS
		
END_CARRY:
		POPF					;restore flags register
CHECK_ADD:
		MOV		Was_ADD,0
		MOV		SI,[BP+2]		;get the next command from Next_CS:Next_IP
		MOV		ES,[BP+4]
		MOV		AX,ES:[SI]
		CMP		AL,03h			;check if the next command is ADD
		JNE		CONT
		MOV		Was_ADD,1		;set Was_ADD=1 if the next command is ADD	
CONT:
		POP		DI				;restore registers & segments
		POP		SI
		POP		DS
		POP		ES
		POP		DX
		POP		CX
		POP		BX
		POP		AX
		MOV		SP,BP		
		POP		BP
		IRET	
_Single_Step_Handler	ENDP
;

_start_track PROC	FAR
		PUSH ES						;Int1Save = getvect(1)
		PUSH AX
		MOV	AX,0
		MOV ES,AX
		MOV AX,ES:[1*4]
		MOV WORD PTR Int1Save,AX
		MOV AX,ES:[1*4+2]
		MOV WORD PTR Int1Save+2,AX
		CLI							;setvect(1,_Single_Step_Handler)
		MOV WORD PTR ES:[1*4],OFFSET _Single_Step_Handler
		MOV WORD PTR ES:[1*4+2],SEG _Single_Step_Handler
		STI
		POP AX
		POP ES
		PUSHF						;TF=1
		POP AX
		OR AX,100000000B
		PUSH AX
		POPF
		RET
_start_track	ENDP
;
_end_track PROC	FAR
		PUSHF						;TF=0
		POP AX
		AND AX,1111111011111111B
		PUSH AX
		POPF
		PUSH ES						;setvect(1,Int1Save)
		PUSH AX
		MOV AX,0
		MOV ES,AX
		CLI
		MOV AX,WORD PTR	Int1Save
		MOV ES:[1*4],AX
		MOV AX,WORD PTR Int1Save+2
		MOV ES:[1*4+2],AX
		STI
		POP AX
		POP ES		
		RET
_end_track	ENDP
		END
