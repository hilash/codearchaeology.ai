#ifndef CODEVISITOR_H_
#define CODEVISITOR_H_

#include "Visitor.h"

class CodeVisitor : public Visitor
{
public:
	static int Label;

	CodeVisitor(ostream & OutputFile_);
	virtual void visit(Node *n);
	virtual void visit(Expr *n);
	virtual void visit(ExprList *n);
	virtual void visit(Dim *n);
	virtual void visit(Atom *n);
	virtual void visit(IntConst *n);
	virtual void visit(RealConst *n);
	virtual void visit(True *n);
	virtual void visit(False *n);
	virtual void visit(Var *n);
	virtual void visit(ArrayRef *n);
	virtual void visit(RecordRef *n);
	virtual void visit(AddressRef *n);
	virtual void visit(Statement *n);
	virtual void visit(Type *n);
	virtual void visit(NewStatement *n);
	virtual void visit(WriteStrStatement *n);
	virtual void visit(WriteVarStatement *n);
	virtual void visit(ProcedureStatement *n);
	virtual void visit(Case *n);
	virtual void visit(CaseList *n);
	virtual void visit(CaseStatement *n);
	virtual void visit(LoopStatement *n);
	virtual void visit(ConditionalStatement *n);
	virtual void visit(Assign *n);
	virtual void visit(StatementList *n);
	virtual void visit(Declaration *n);
	virtual void visit(VariableDeclaration *n);
	virtual void visit(RecordList *n);
	virtual void visit(SimpleType *n);
	virtual void visit(IdeType *n);
	virtual void visit(ArrayType *n);
	virtual void visit(RecordType *n);
	virtual void visit(AddressType *n);
	virtual void visit(Parameter *n);
	virtual void visit(ByReferenceParameter *n);
	virtual void visit(ParameterList *n);
	virtual void visit(FunctionDeclaration *n);
	virtual void visit(ProcedureDeclaration *n);
	virtual void visit(DeclarationList *n);
	virtual void visit(Block *n);
	virtual void visit(Program *n);
	
	virtual ~CodeVisitor();
};

#endif /*CODEVISITOR_H_*/
