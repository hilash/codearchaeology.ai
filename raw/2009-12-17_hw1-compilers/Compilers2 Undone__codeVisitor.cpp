#include "codeVisitor.h"
#include "ast.h"
#include <fstream>

int CodeVisitor::LabelIf=0;
int CodeVisitor::LabelCaseCurrent=0;
int CodeVisitor::LabelCaseNextAvailable=0;
int CodeVisitor::LabelCasei=0;
bool* Visitor::IsInitTable=NULL;
map<string,int>* Visitor::Table=NULL;

void CodeVisitor::visit(Node *n){
}
void CodeVisitor::visit(Expr *n){
}
void CodeVisitor::visit(ExprList *n){
}
void CodeVisitor::visit(Dim *n){
}
void CodeVisitor::visit(Atom *n){
}
void CodeVisitor::visit(IntConst *n){
}
void CodeVisitor::visit(RealConst *n){
}
void CodeVisitor::visit(True *n){
}
void CodeVisitor::visit(False *n){
}
void CodeVisitor::visit(Var *n){
}
void CodeVisitor::visit(ArrayRef *n){
}
void CodeVisitor::visit(RecordRef *n){
}
void CodeVisitor::visit(AddressRef *n){
}
void CodeVisitor::visit(Statement *n){
}
void CodeVisitor::visit(Type *n){
}
void CodeVisitor::visit(NewStatement *n){
}
void CodeVisitor::visit(WriteStrStatement *n){
}
void CodeVisitor::visit(WriteVarStatement *n){
}
void CodeVisitor::visit(ProcedureStatement *n){
}
void CodeVisitor::visit(Case *n){
	
	output <<"Node name : Case";

	if (n->stat_list_!=NULL) 
			n->stat_list_->accept(code);

	if (n->leafChild_!=NULL) 
	{
		//n->leafChild_->print(output);
		n->leafChild_->accept(code);
	}
}
void CodeVisitor::visit(CaseList *n){
	int i=LabelCasei++;
	
	output << "Node name : CaseList";

	cout << "LBL_CASE_" << LabelCaseCurrent << "_" << i << "\n";

	// print the case operations
	if (n->case_!=NULL) 
			n->case_->accept(code);

	// ujp to the end of case
	cout << "ujp LBL_CASE_" << LabelCaseCurrent <<  "\n";

	// print other cases
	if (n->case_list_!=NULL) 
			n->case_list_->accept(code);

	cout << "ujp LBL_CASE_" << LabelCaseCurrent << "_" << i << "\n";
}
void CodeVisitor::visit(CaseStatement *n){
	int x = LabelCaseNextAvailable++;
	LabelCasei=0;
	
	output << "Node name : CaseStatement";
	
	//put the value of EXP in the stack
	if (n->exp_!=NULL) 
			n->exp_->accept(codeRight);

	cout << "neg i\n";
	cout << "ixj LBL_CASE_" << x << "\n";

	if (n->case_list_!=NULL) 
			n->case_list_->accept(code);

	cout << "LBL_CASE_" << x << "\n";
}
void CodeVisitor::visit(LoopStatement *n){
	static int LoopLabel=0;
	output << "Node name : LoopStatement";
	cout << "LBL_LOOP_" << LoopLabel << "\n";
	if (n->exp_!=NULL) 
			n->exp_->accept(codeRight);
	cout << "fjp LBL_LOOP_" << LoopLabel+1 << "\n";
	if (n->stat_list_!=NULL) 
			n->stat_list_->accept(code);
	cout << "ujp LBL_LOOP_" << LoopLabel << "\n";
	cout << "LBL_LOOP_" << LoopLabel+1 << "\n";
	LoopLabel+=2;
}
void CodeVisitor::visit(ConditionalStatement *n){
	output << "Node name : ConditionalStatement";

	// IF exp_ THEN stat_list_if_ FI
	if (n->stat_list_else_==NULL)
	{
		int x=LabelIf++;

		if (n->exp_!=NULL) 
			n->exp_->accept(codeRight);
		
		cout << "fjp LBL_IF_" << x << "\n";

		if (n->stat_list_if_!=NULL) 
			n->stat_list_if_->accept(code);
		
		cout << "LBL_IF_" << x << "\n";
	}

	// IF exp_ THEN stat_list_if_ ELSE stat_list_else_ FI
	if (n->stat_list_else_!=NULL)
	{
		int x=LabelIf;
		LabelIf+=2;
	
		if (n->exp_!=NULL) 
			n->exp_->accept(codeRight);
		
		cout << "fjp LBL_IF_" << x++ << "\n";

		if (n->stat_list_if_!=NULL) 
			n->stat_list_if_->accept(code);
		
		cout << "fjp LBL_IF_" << x-- << "\n";

		cout << "LBL_IF_" << x++ << "\n";

		if (n->stat_list_else_!=NULL) 
			n->stat_list_else_->accept(code);

		cout << "LBL_IF_" << x << "\n";
	}

}
void CodeVisitor::visit(Assign *n){
	output<<"Node name : Assign";
	if (n->var_!=NULL) 
		n->var_->accept(codeLeft);
	 if (n->exp_!=NULL) 
		 n->exp_->accept(codeRight);
	 cout << "sto i\n";
}
void CodeVisitor::visit(StatementList *n){

	output <<"Node name : StatementList \n";
	if (n->stat_!=NULL)
		n->stat_->accept(this);
	if (n->stat_list_!=NULL)
		n->stat_list_->accept(this);	
}
void CodeVisitor::visit(Declaration *n){
}
void CodeVisitor::visit(VariableDeclaration *n){
	static int adress = 5;

	output <<"Node name : VariableDeclaration. Var name is: " << *(n->name_) << "\n";
	if (n->type_!=NULL)
		n->type_->accept(this);

	(*Table)[*(n->name_)]=adress++;

}
void CodeVisitor::visit(RecordList *n){
}
void CodeVisitor::visit(SimpleType *n){
	output<<"Node name : SimpleType \n";
	output<<"Type is : "<< *(n->name_) << "\n";
}
void CodeVisitor::visit(IdeType *n){
}
void CodeVisitor::visit(ArrayType *n){
}
void CodeVisitor::visit(RecordType *n){
}
void CodeVisitor::visit(AddressType *n){
}
void CodeVisitor::visit(Parameter *n){
}
void CodeVisitor::visit(ByReferenceParameter *n){
}
void CodeVisitor::visit(ParameterList *n){
}
void CodeVisitor::visit(FunctionDeclaration *n){
}
void CodeVisitor::visit(ProcedureDeclaration *n){
}
void CodeVisitor::visit(DeclarationList *n){
	output <<"Node name : DeclarationList\n";
	if (n->decl_list_!=NULL)
		n->decl_list_->accept(this);
	if (n->decl_!=NULL)
		n->decl_->accept(this);
}
void CodeVisitor::visit(Block *n){
	output <<"Node name : Begin\n";

	if (n->decl_list_!=NULL)
		n->decl_list_->accept(this);

	if (n->stat_seq_!=NULL)
		n->stat_seq_->accept(this);

	//map<string,int>::iterator it;
	// print the symbole table
	/*for ( it=(*Table).begin() ; it != (*Table).end(); it++ )
    cout << (*it).first << " => " << (*it).second << "\n";*/
}
void CodeVisitor::visit(Program *n){
	//output<<"this is a print example for program \"";
	output << "==================  Abstract Syntax Tree ================\n";
	output <<"Node name : Root/Program. Program name is: "<<*(n->name_)<<"\n";
	//output <<"this is where you start changing the code\n";

	/**
	 * here is a way to call the visitor
	 */
	n->block_->accept(this);
	/* here are some other options:
	 * n->block_->accept(code);
	 * n->block_->accept(codeRight);
	 *n->block_->accept(codeLeft);
	 **/
	cout << "stp\n";
}

/**
 OutputFile_ is the file that !you! (the students),
 write your output to;
 */
CodeVisitor::CodeVisitor(ostream& OutputFile_):
Visitor(OutputFile_)
{
	if (IsInitTable==NULL)
	{
		IsInitTable = new bool;
		*IsInitTable = false;
	}
	if ((*IsInitTable)==false)
	{
		(*IsInitTable)=true;

		//cout << "good";
		Table = new map<string,int>;
	}
	else
	{
		//cout << "bad";
	}
}

CodeVisitor::~CodeVisitor()
{
}
