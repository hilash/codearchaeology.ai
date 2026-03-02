#ifndef VISITOR_H_
#define VISITOR_H_
#include <iostream>
#include <fstream>
#include <map>
#include <string>
using namespace std;

class Node ;
class Expr ;
class ExprList ;
class Dim ;
class Atom ;
class IntConst ;
class RealConst ;
class True ;
class False ;
class Var ;
class ArrayRef ;
class RecordRef ;
class AddressRef ;
class Statement ;
class Type ;
class NewStatement ;
class WriteStrStatement ;
class WriteVarStatement ;
class ProcedureStatement ;
class Case ;
class CaseList ;
class CaseStatement ;
class LoopStatement ;
class ConditionalStatement ;
class Assign ;
class StatementList ;
class Declaration ;
class VariableDeclaration ;
class RecordList ;
class SimpleType ;
class IdeType ;
class ArrayType ;
class RecordType ;
class AddressType ;
class Parameter ;
class ByReferenceParameter ;
class ParameterList ;
class FunctionDeclaration ;
class ProcedureDeclaration ;
class DeclarationList ;
class Block ;
class Program ;

class Visitor
{
	public:
		ostream  &output;
		static Visitor *codeLeft;
		static Visitor *codeRight;
		static Visitor *code;
		static map<string,int> *Table;
		static bool *IsInitTable;
		
		Visitor(ostream & OutputFile_):output(OutputFile_){
		}
		virtual void visit(Node *n)=0;
		virtual void visit(Expr *n)=0;
		virtual void visit(ExprList *n)=0;
		virtual void visit(Dim *n)=0;
		virtual void visit(Atom *n)=0;
		virtual void visit(IntConst *n)=0;
		virtual void visit(RealConst *n)=0;
		virtual void visit(True *n)=0;
		virtual void visit(False *n)=0;
		virtual void visit(Var *n)=0;
		virtual void visit(ArrayRef *n)=0;
		virtual void visit(RecordRef *n)=0;
		virtual void visit(AddressRef *n)=0;
		virtual void visit(Statement *n)=0;
		virtual void visit(Type *n)=0;
		virtual void visit(NewStatement *n)=0;
		virtual void visit(WriteStrStatement *n)=0;
		virtual void visit(WriteVarStatement *n)=0;
		virtual void visit(ProcedureStatement *n)=0;
		virtual void visit(Case *n)=0;
		virtual void visit(CaseList *n)=0;
		virtual void visit(CaseStatement *n)=0;
		virtual void visit(LoopStatement *n)=0;
		virtual void visit(ConditionalStatement *n)=0;
		virtual void visit(Assign *n)=0;
		virtual void visit(StatementList *n)=0;
		virtual void visit(Declaration *n)=0;
		virtual void visit(VariableDeclaration *n)=0;
		virtual void visit(RecordList *n)=0;
		virtual void visit(SimpleType *n)=0;
		virtual void visit(IdeType *n)=0;
		virtual void visit(ArrayType *n)=0;
		virtual void visit(RecordType *n)=0;
		virtual void visit(AddressType *n)=0;
		virtual void visit(Parameter *n)=0;
		virtual void visit(ByReferenceParameter *n)=0;
		virtual void visit(ParameterList *n)=0;
		virtual void visit(FunctionDeclaration *n)=0;
		virtual void visit(ProcedureDeclaration *n)=0;
		virtual void visit(DeclarationList *n)=0;
		virtual void visit(Block *n)=0;
		virtual void visit(Program *n)=0;
};

#endif /*VISITOR_H_*/
