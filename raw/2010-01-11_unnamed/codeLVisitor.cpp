#include "codeLVisitor.h"
#include "ast.h"

void CodeLVisitor::visit(Node *n){
}
void CodeLVisitor::visit(Expr *n){
}
void CodeLVisitor::visit(ExprList *n){
}
void CodeLVisitor::visit(Dim *n){
}
void CodeLVisitor::visit(Atom *n){
}
void CodeLVisitor::visit(IntConst *n){
}
void CodeLVisitor::visit(RealConst *n){
}
void CodeLVisitor::visit(True *n){
}
void CodeLVisitor::visit(False *n){
}
void CodeLVisitor::visit(Var *n){
}
void CodeLVisitor::visit(ArrayRef *n){
}
void CodeLVisitor::visit(RecordRef *n){
}
void CodeLVisitor::visit(AddressRef *n){
}
void CodeLVisitor::visit(Statement *n){
}
void CodeLVisitor::visit(Type *n){
}
void CodeLVisitor::visit(NewStatement *n){
}
void CodeLVisitor::visit(WriteStrStatement *n){
}
void CodeLVisitor::visit(WriteVarStatement *n){
}
void CodeLVisitor::visit(ProcedureStatement *n){
}
void CodeLVisitor::visit(Case *n){
}
void CodeLVisitor::visit(CaseList *n){
}
void CodeLVisitor::visit(CaseStatement *n){
}
void CodeLVisitor::visit(LoopStatement *n){
}
void CodeLVisitor::visit(ConditionalStatement *n){
}
void CodeLVisitor::visit(Assign *n){
}
void CodeLVisitor::visit(StatementList *n){
}
void CodeLVisitor::visit(Declaration *n){
}
void CodeLVisitor::visit(VariableDeclaration *n){
}
void CodeLVisitor::visit(RecordList *n){
}
void CodeLVisitor::visit(SimpleType *n){
}
void CodeLVisitor::visit(IdeType *n){

	/*map<string,int>::iterator it;
	cout << "TABLE:\n";
	for ( it=(*Table).begin() ; it != (*Table).end(); it++ )
		cout << (*it).first << " => " << (*it).second << "\n";
	cout << "***********\n";*/

	int x = (*Table)[*(n->name_)];
	//output << "Node name : IdeType\n";
	output << "ldc a "<< x << " \n";	
}
void CodeLVisitor::visit(ArrayType *n){
}
void CodeLVisitor::visit(RecordType *n){
}
void CodeLVisitor::visit(AddressType *n){
}
void CodeLVisitor::visit(Parameter *n){
}
void CodeLVisitor::visit(ByReferenceParameter *n){
}
void CodeLVisitor::visit(ParameterList *n){
}
void CodeLVisitor::visit(FunctionDeclaration *n){
}
void CodeLVisitor::visit(ProcedureDeclaration *n){
}
void CodeLVisitor::visit(DeclarationList *n){
}
void CodeLVisitor::visit(Block *n){
}
void CodeLVisitor::visit(Program *n){
}

/**
 OutputFile_ is the file that !you! (the students),
 write your output to;
 */
CodeLVisitor::CodeLVisitor(ostream & OutputFile_):
Visitor(OutputFile_)
{
}

CodeLVisitor::~CodeLVisitor()
{
}
