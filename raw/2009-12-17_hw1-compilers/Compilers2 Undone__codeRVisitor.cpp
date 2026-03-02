#include "codeRVisitor.h"
#include "ast.h"

void CodeRVisitor::visit(Node *n){
}
void CodeRVisitor::visit(Expr *n){
	output<<"Node name : Expr\n";

    if (n->unary_){
      output<<"Unary op is : "<<n->op_;
      //call to the operand
	  if (n->atom_!=NULL)
		n->atom_->accept(codeRight);
	  
	  switch ( n->op_ )
	  {
		case MIN:
            cout << "neg i\n";
            break;
		case NOT:
			cout << "not\n";
            break;
         default:
            cout << "\n";
      }
    }
	else
	{
      output<<"Binary op is : "<<n->op_;

	  if (n->left_!=NULL)
		 n->left_->accept(codeRight);

	  if (n->right_!=NULL)
		 n->right_->accept(codeRight);
	  
	  switch ( n->op_ )
	  {
		case ADD:
			cout << "add i\n";
            break;
		case MIN:
            cout << "sub i\n";
            break;
		case MUL:
			cout << "mul i\n";
            break;
        case AND:
            cout << "and\n";
            break;
		case OR:
			cout << "or\n";
            break;
		case EQU:
            cout << "equ i\n";
            break;
		case GEQ:
            cout << "geq i\n";
            break;
        case LEQ:
            cout << "leq i\n";
            break;
		case LES:
			cout << "les i\n";
            break;
		case GRE:
            cout << "grt i\n";
            break;
		case NEQ:
            cout << "neq i\n";
            break;
         default:
            cout << "\n";
      }

    }

}
void CodeRVisitor::visit(ExprList *n){
}
void CodeRVisitor::visit(Dim *n){
}
void CodeRVisitor::visit(Atom *n){
}
void CodeRVisitor::visit(IntConst *n){
	output <<"Node name : IntConst. Value is :"<< n->i_<<"\n";
	cout << "ldc i " << n->i_ << "\n";
}
void CodeRVisitor::visit(RealConst *n){
}
void CodeRVisitor::visit(True *n){
}
void CodeRVisitor::visit(False *n){
}
void CodeRVisitor::visit(Var *n){
}
void CodeRVisitor::visit(ArrayRef *n){
}
void CodeRVisitor::visit(RecordRef *n){
}
void CodeRVisitor::visit(AddressRef *n){
}
void CodeRVisitor::visit(Statement *n){
}
void CodeRVisitor::visit(Type *n){
}
void CodeRVisitor::visit(NewStatement *n){
}
void CodeRVisitor::visit(WriteStrStatement *n){
}
void CodeRVisitor::visit(WriteVarStatement *n){
}
void CodeRVisitor::visit(ProcedureStatement *n){
}
void CodeRVisitor::visit(Case *n){
}
void CodeRVisitor::visit(CaseList *n){
}
void CodeRVisitor::visit(CaseStatement *n){
}
void CodeRVisitor::visit(LoopStatement *n){
}
void CodeRVisitor::visit(ConditionalStatement *n){
}
void CodeRVisitor::visit(Assign *n){
}
void CodeRVisitor::visit(StatementList *n){
}
void CodeRVisitor::visit(Declaration *n){
}
void CodeRVisitor::visit(VariableDeclaration *n){
}
void CodeRVisitor::visit(RecordList *n){
}
void CodeRVisitor::visit(SimpleType *n){
}
void CodeRVisitor::visit(IdeType *n){
	// bEFORE:
	//n->accept(codeLeft);
	//output << "ind i\n";

	///FIXED VERSION:
	// just get the value of x - get it's adress
	int ad = (*Table)[*(n->name_)];
	output << "Node name : IdeType\n";
	cout << "ldo i " << ad << "\n";
}
void CodeRVisitor::visit(ArrayType *n){
}
void CodeRVisitor::visit(RecordType *n){
}
void CodeRVisitor::visit(AddressType *n){
}
void CodeRVisitor::visit(Parameter *n){
}
void CodeRVisitor::visit(ByReferenceParameter *n){
}
void CodeRVisitor::visit(ParameterList *n){
}
void CodeRVisitor::visit(FunctionDeclaration *n){
}
void CodeRVisitor::visit(ProcedureDeclaration *n){
}
void CodeRVisitor::visit(DeclarationList *n){
}
void CodeRVisitor::visit(Block *n){
}
void CodeRVisitor::visit(Program *n){
}

/**
 OutputFile_ is the file that !you! (the students),
 write your output to;
 */
CodeRVisitor::CodeRVisitor(ostream & OutputFile_):
Visitor(OutputFile_)
{
}

CodeRVisitor::~CodeRVisitor()
{
}
