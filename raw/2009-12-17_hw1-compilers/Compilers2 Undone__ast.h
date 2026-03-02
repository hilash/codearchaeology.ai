/**************************************
 *
 * University of Haifa
 *
 * Theory of compilation
 *
 * P machine compiler - code generation project
 *
 * Yosi Ben Asher
 * Email: 
 *
 * Omer Boehm
 * Email: 
 *
 **************************************/

#ifndef AST_H
#define AST_H

#include <iostream>
#include <assert.h>
#include <string>

#include "typedef.h"
#include "visitor.h"

using namespace std;
/**
 * classes
 */

// base class for tree nodes
class Node {
public:
 virtual ~Node () {}

 virtual void print (ostream& os) const = 0;
 virtual Node * clone () const {return NULL;}

 virtual void accept(Visitor * v)=0;
};

class Expr : public Node {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  // Unary operations
  Expr (int op, Node * atom) : op_(op), atom_(atom), left_(NULL), right_(NULL), unary_(true) {}
  // Binary operations
  Expr (int op, Node * left, Node * right) : op_(op), left_(left), right_(right), atom_(NULL), unary_(false) {}

  Expr(const Expr & exp){
    unary_= exp.unary_;
    op_ = exp.op_;
    left_ = exp.left_->clone();
    right_ = exp.right_->clone();
    atom_ = exp.atom_->clone();
  }

  virtual ~Expr(){
    if (left_) delete left_;
    if (right_) delete right_;
    if (atom_) delete atom_;
  }


  void print (ostream& os) const {
    os<<"Node name : Expr"<<endl;
    assert(op_);
    if (unary_){
      os<<"Unary op is : "<<op_;
      assert(atom_);
      atom_->print(os);
    }else{
      os<<"Binary op is : "<<op_;
      assert(left_ && right_);
      left_->print(os);
      right_->print(os);
    }
  }

  Node * clone () const { return new Expr(*this);}

public:
  bool unary_;
  int op_;
  Node * left_;
  Node * right_;
  Node * atom_;
};

class ExprList : public Node {
public :
 	void accept(Visitor * v){
		v->visit(this);
	}
  ExprList (Node * expr) : expr_(expr),expr_list_(NULL){assert(expr_);}
  ExprList (Node * expr, Node * expr_list) : expr_(expr),expr_list_(expr_list) {assert(expr_ && expr_list_);}

  ExprList(const ExprList& exp){
    expr_ = exp.expr_->clone();
    expr_list_ = exp.expr_list_->clone();
  }

  virtual ~ExprList(){
    if (expr_) delete expr_;
    if (expr_list_) delete expr_list_;
  }

  void print (ostream& os) const {
		os<<"Node name : ExprList";
		assert( expr_);
    expr_->print(os);
    if (expr_list_){
      expr_list_->print(os);
    }
  }

  Node * clone () const { return new ExprList(*this);}

public:
  Node * expr_;
	Node * expr_list_;
};

class Dim : public Node {
public:
	void accept(Visitor * v){
		v->visit(this);
	}
  Dim (Node * exp) : exp_(exp), dim_(NULL) {assert(exp_);}
  Dim (Node * exp, Node * dim) : exp_(exp),dim_(dim) {assert(exp_ && dim_);}

  Dim(const Dim& d){
    exp_ = d.exp_->clone();
    dim_ = d.dim_->clone();
  }

  virtual ~Dim(){
    if (exp_) delete exp_;
    if (dim_) delete dim_;
  }

  void print (ostream& os) const {
    os<<"Node name : Dim"<<endl;
    assert(exp_);
    exp_->print(os);
    if (dim_){
      dim_->print(os);
    }
  }

  Node * clone () const { return new Dim(*this);}

public:
  Node * exp_;
  Node * dim_;
};

class Atom : public Node {
	void accept(Visitor * v){
		v->visit(this);
	}
};

class IntConst : public Atom {
public:
	void accept(Visitor * v){
		v->visit(this);
	}
  IntConst(const int i) : i_(i) {}
  IntConst(const IntConst& in) : i_(in.i_) {}

  void print (ostream& os) const {
    os<<"Node name : IntConst. Value is :"<<i_<<endl;
  }

  Node * clone () const { return new IntConst(*this);}

public:
  const int i_;
};

class RealConst : public Atom {
public:
	void accept(Visitor * v){
		v->visit(this);
	}
  RealConst(const double r) : r_(r) {}
  RealConst(const RealConst& in) : r_(in.r_) {}

  void print (ostream& os) const {
    os<<"Node name : RealConst. Value is :"<<r_<<endl;
  }

  Node * clone () const { return new RealConst(*this);}

public:
  const double r_;
};

class True : public Atom {
public:
	void accept(Visitor * v){
		v->visit(this);
	}
  void print (ostream& os) const {
    os<<"Node name : trueConst. Value is true"<<endl;
  }

  Node * clone () const { return new True();}

};

class False : public Atom {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  void print (ostream& os) const {
    os<<"Node name : trueConst. Value is false"<<endl;
  }
  Node * clone () const { return new False();}
};

class Var : public Atom {
	void accept(Visitor * v){
		v->visit(this);
	}
};

class ArrayRef : public Var {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  ArrayRef (Node * var, Node * dim) : var_(var),dim_(dim) {assert(var_ && dim_);}
  ArrayRef(const ArrayRef& arr){
    var_ = arr.var_->clone();
    dim_ = arr.dim_->clone();
  }

  virtual ~ArrayRef(){
    if (var_) delete var_;
    if (dim_) delete dim_;
  }

  void print (ostream& os) const {
    os<<"Node name : ArrayRef"<<endl;
    assert(var_ && dim_);
    var_->print(os);
    dim_->print(os);
  }

  Node * clone () const { return new ArrayRef(*this);}

public:
  Node * var_;
  Node * dim_;
};

class RecordRef : public Var {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  RecordRef (Node * varExt, Node * varIn) : varExt_(varExt),varIn_(varIn) {assert(varExt_ && varIn_);}
  RecordRef(const RecordRef& rec){
    varExt_ = rec.varExt_->clone();
    varIn_ = rec.varIn_->clone();
  }

  virtual ~RecordRef(){
    if (varExt_) delete varExt_;
    if (varIn_) delete varIn_;
  }

  void print (ostream& os) const {
    os<<"Node name : RecordRef"<<endl;
    assert(varExt_ && varIn_);
    varExt_->print(os);
    varIn_->print(os);
  }

  Node * clone () const { return new RecordRef(*this);}

public:
  Node * varExt_;
  Node * varIn_;
};

class AddressRef : public Var {
public :
 	void accept(Visitor * v){
		v->visit(this);
	}
  AddressRef (Node * var) : var_(var) {assert(var_);}
  AddressRef(const AddressRef& addre){
    var_ = addre.var_->clone();
  }

  virtual ~AddressRef(){
    if (var_) delete var_;
  }

  void print (ostream& os) const {
    os<<"Node name : AddressRef"<<endl;
    assert(var_);
    var_->print(os);
  }

  Node * clone () { return new AddressRef(*this);}

public:
  Node * var_;
};

class Statement : public Node {
	void accept(Visitor * v){
		v->visit(this);
	}
};

class Type : public Node {
	void accept(Visitor * v){
		v->visit(this);
	}
};

class NewStatement : public Statement {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  NewStatement (Node * var) : var_(var) {assert(var_);}
  NewStatement(const NewStatement& ns){
    var_ = ns.var_->clone();
  }

  virtual ~NewStatement(){
    if (var_) delete var_;
  }

  void print (ostream& os) const {
		os<<"Node name : NewStatement";
		assert(var_);
    var_->print(os);
  }

  Node * clone () { return new NewStatement(*this);}

public:
  Node * var_;
};

class WriteStrStatement : public Statement {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  WriteStrStatement (const char * str) {
		str_ = new string(str);
	}

  WriteStrStatement(const WriteStrStatement& ns){
    str_ = new string(*ns.str_);
  }

  virtual ~WriteStrStatement () {
		if (str_) delete str_;
	}
  void print (ostream& os) const {
		os<<"Node name : WriteStrStatement";
		assert(str_);
    os<<"Str statement is: "<<str_<<endl;
  }

  Node * clone () { return new WriteStrStatement(*this);}

public:
  string * str_;
};

class WriteVarStatement : public Statement {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  WriteVarStatement (Node * exp) : exp_(exp) {assert(exp_);}

  WriteVarStatement(const WriteVarStatement& ex){
    exp_ = ex.clone();
  }

  virtual ~WriteVarStatement(){
    if (exp_) delete exp_;
  }

  void print (ostream& os) const {
		os<<"Node name : WriteVarStatement";
		assert(exp_);
    exp_->print(os);
  }

  Node * clone () const { return new WriteVarStatement(*this);}

public:
  Node * exp_;
};

class ProcedureStatement : public Statement {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  ProcedureStatement (const char * str) {
		str_ = new string(str);
	}

  ProcedureStatement (Node * expr_list, const char * str) : expr_list_(expr_list) {
    assert(expr_list_);
    str_ = new string(str);
  }

  ProcedureStatement(const ProcedureStatement& ps){
    expr_list_ = ps.expr_list_->clone();
    str_ = new string(*ps.str_);
  }

   virtual ~ProcedureStatement () {
		if (str_) delete str_;
    if (expr_list_) delete expr_list_;
	}

  void print (ostream& os) const {
    os<<"Node name : ProcedureStatement. Proc name : "<<str_<<endl;
    if (expr_list_ ){
      expr_list_->print(os);
    }
  }

  Node * clone () const { return new ProcedureStatement(*this);}

public:
	Node * expr_list_;
  string * str_;
};

class Case : public Node {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  Case (Node * stat_list, int val) : leafChild_ (NULL), stat_list_(stat_list) {
		// note the special treatment in miny.y for this case (makenode)
		leafChild_ = new IntConst(val);
		assert(stat_list_);
	}

  Case(const Case& c){
    stat_list_ = c.stat_list_->clone();
    leafChild_ = c.leafChild_->clone();
  }

  virtual ~Case () {
    if (stat_list_) delete stat_list_;
    if (leafChild_) delete leafChild_;
	}

  void print (ostream& os) const {
		os<<"Node name : Case";
		assert(stat_list_);
	  stat_list_->print(os);
  }

  Node * clone () const { return new Case(*this);}

public:
  Node * stat_list_;
	Node * leafChild_;
};


class CaseList : public Node {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  CaseList (Node * ccase) : case_(ccase),case_list_(NULL) {assert(case_);}
  CaseList (Node * ccase, Node * case_list) : case_(ccase),case_list_(case_list) {assert(case_ && case_list_);}

  CaseList(const CaseList& cl){
    case_ = cl.case_->clone();
    case_list_ = cl.case_list_->clone();
  }

  virtual ~CaseList () {
    if (case_) delete case_;
    if (case_list_) delete case_list_;
	}

  void print (ostream& os) const {
		os<<"Node name : CaseList";
		assert( case_ );
    case_->print(os);
    if (case_list_){
      case_list_->print(os);
    }
  }

  Node * clone () const { return new CaseList(*this);}

public:
  Node * case_;
	Node * case_list_;
};

class CaseStatement : public Statement {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  CaseStatement (Node * exp, Node * case_list) : exp_(exp),case_list_(case_list) {assert(exp_ && case_list_);}

  CaseStatement(const CaseStatement& cs){
    exp_ = cs.exp_->clone();
    case_list_ = cs.case_list_->clone();
  }

  virtual ~CaseStatement () {
    if (exp_) delete exp_;
    if (case_list_) delete case_list_;
	}

  void print (ostream& os) const {
		os<<"Node name : CaseStatement";
		assert( exp_ && case_list_);
    exp_->print(os);
		case_list_->print(os);
  }

  Node * clone () const { return new CaseStatement(*this);}

public:
  Node * exp_;
	Node * case_list_;
};

class LoopStatement : public Statement {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  LoopStatement (Node * exp, Node * stat_list) : exp_(exp),stat_list_(stat_list) {assert(exp_ && stat_list_);}

  LoopStatement(const LoopStatement& ls){
    exp_ = ls.exp_->clone();
    stat_list_ = ls.stat_list_->clone();
  }

  virtual ~LoopStatement () {
    if (exp_) delete exp_;
    if (stat_list_) delete stat_list_;
	}

  void print (ostream& os) const {
		os<<"Node name : LoopStatement";
		assert( exp_ && stat_list_);
    exp_->print(os);
		stat_list_->print(os);
  }

  Node * clone () const { return new LoopStatement(*this);}

public:
  Node * exp_;
	Node * stat_list_;
};


class ConditionalStatement : public Statement {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  ConditionalStatement (Node * exp, Node * stat_list_if) : exp_(exp),stat_list_if_(stat_list_if), stat_list_else_(NULL) {assert(exp_ && stat_list_if_);}
  ConditionalStatement (Node * exp, Node * stat_list_if, Node * stat_list_else) : exp_(exp),stat_list_if_(stat_list_if), stat_list_else_(stat_list_else) {assert(exp_ && stat_list_if_ && stat_list_else_ );}

  ConditionalStatement(const ConditionalStatement& cs){
    exp_ = cs.exp_->clone();
    stat_list_if_ = cs.stat_list_if_->clone();
    stat_list_else_ = cs.stat_list_else_->clone();
  }

  virtual ~ConditionalStatement () {
    if (exp_) delete exp_;
    if (stat_list_if_) delete stat_list_if_;
    if (stat_list_else_) delete stat_list_else_;
	}

  void print (ostream& os) const {
		os<<"Node name : ConditionalStatement";
		assert( exp_ && stat_list_if_);
    exp_->print(os);
		stat_list_if_->print(os);
		if (stat_list_else_){
			stat_list_else_->print(os);
		}
  }

  Node * clone () const { return new ConditionalStatement(*this);}

public:
  Node * exp_;
	Node * stat_list_if_;
	Node * stat_list_else_;
};


class Assign : public Statement {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  Assign (Node * exp, Node * var) :exp_(exp), var_(var)  {assert(var_ && exp_);}

  Assign(const Assign& as){
    var_ = as.var_->clone();
    exp_ = as.exp_->clone();
  }

  virtual ~Assign () {
    if (exp_) delete exp_;
    if (var_) delete var_;
	}

  void print (ostream& os) const {
		os<<"Node name : Assign";
		assert(var_ && exp_);
    var_->print(os);
    exp_->print(os);
  }

  Node * clone () const { return new Assign(*this);}

public:
  Node * var_;
  Node * exp_;
};

class StatementList : public Node {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  StatementList (Node * stat) : stat_list_(NULL), stat_(stat)  { assert(stat_);}
  StatementList (Node * stat, Node * stat_list) : stat_(stat),stat_list_(stat_list)  {assert(stat_list_ && stat);}

  StatementList (const StatementList& sl){
    stat_ = sl.stat_->clone();
    stat_list_ = sl.stat_list_->clone();
  }

  virtual ~StatementList () {
    if (stat_) delete stat_;
    if (stat_list_) delete stat_list_;
	}

  void print (ostream& os) const {
	os<<"Node name : StatementList"<<endl;

	assert(stat_);
	stat_->print(os);
	if (stat_list_){
		stat_list_->print(os);
	}
  }


  Node * clone () const { return new StatementList(*this);}

public:
  Node * stat_;
  Node * stat_list_;
};

class Declaration : public Node {
	void accept(Visitor * v){
		v->visit(this);
	}
};

class VariableDeclaration : public Declaration {
public:
	void accept(Visitor * v){
		v->visit(this);
	}
  VariableDeclaration (Node * type, const char * str) : type_(type){
    assert(type_);
    name_ = new string(str);
  }

  VariableDeclaration(const VariableDeclaration& p){
    type_ = p.type_->clone();
    name_ = new string(*p.name_);
  }

  virtual ~VariableDeclaration () {
    if (type_) delete type_;
		delete name_;
	}

  void print (ostream& os) const {
    os<<"Node name : VariableDeclaration. Var name is: "<<*name_<<endl;
		assert(type_);
	  type_->print(os);
  }

  Node * clone () const { return new VariableDeclaration(*this);}

public:
  Node * type_;
  string * name_;
};


class RecordList : public Node {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  RecordList (Node * var_decl) : record_list_(NULL), var_decl_(var_decl)  { assert(var_decl_);}
  RecordList (Node * var_decl, Node * record_list) : record_list_(record_list),var_decl_(var_decl)  {assert(record_list_ && var_decl);}

  RecordList(const RecordList& li){
    var_decl_= li.var_decl_->clone();
    record_list_ = li.record_list_->clone();
  }

  virtual ~RecordList () {
    if (var_decl_) delete var_decl_;
    if (record_list_) delete record_list_;
	}

  void print (ostream& os) const {
	  os<<"Node name : RecordList"<<endl;
	  if (record_list_){
		  record_list_->print(os);
	  }
	  assert(var_decl_);
	  var_decl_->print(os);
  }

  Node * clone () const { return new RecordList(*this);}

public:
  Node * var_decl_;
  Node * record_list_;
};

class SimpleType : public Type {
public:
	void accept(Visitor * v){
		v->visit(this);
	}
  SimpleType (const char * name) {
		name_ = new string(name);
	}

  virtual ~SimpleType () {
		if (name_ )delete name_;
	}

  SimpleType(const SimpleType& s){
    name_ = new string(*s.name_);
  }

  void print (ostream& os) const {
		os<<"Node name : SimpleType"<<endl;;
		os<<"Type is : "<< (*name_) <<endl;
	}

  Node * clone () const { return new SimpleType(*this);}

public:
  string * name_;
};

class IdeType : public Type {
public:
	void accept(Visitor * v){
		v->visit(this);
	}
  IdeType (const char * name) {
    name_ = new string(name);
  }

  virtual ~IdeType () {
    if (name_) delete name_;
  }

  IdeType(const IdeType& s){
    name_ = new string(*s.name_);
  }

  void print (ostream& os) const {
    os<<"Node name : IdeType"<<endl;
  }

  Node * clone () const { return new IdeType(*this);}

public:
  string * name_;
};

class ArrayType : public Type {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  ArrayType (int l,int u, Node * type) : low_(l),up_(u),type_(type)  { assert(type_);}

  ArrayType(const ArrayType& a) : low_(a.low_), up_(a.up_){
    type_ = a.type_->clone();
  }

  virtual ~ArrayType () {
    if (type_) delete type_;
  }

  void print (ostream& os) const {
		os<<"Node name : ArrayType: low bound is: "<<low_<<", up bound is: "<<up_<<endl;
	  assert(type_);
	  type_->print(os);
  }

  Node * clone () const { return new ArrayType(*this);}

public:
  Node * type_;
	int low_,up_;
};

class RecordType : public Type {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  RecordType (Node * record_list) : record_list_(record_list)  { assert(record_list_);}

  RecordType(const RecordType& y){
    record_list_ = y.record_list_->clone();
  }

  virtual ~RecordType () {
    if (record_list_) delete record_list_;
  }

  void print (ostream& os) const {
		os<<"Node name : RecordType"<<endl;
	  assert(record_list_);
	  record_list_->print(os);
  }

  Node * clone () const { return new RecordType(*this);}

public:
  Node * record_list_;
};


class AddressType : public Type {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  AddressType (Node * type) : type_(type)  { assert(type_);}

  AddressType(const AddressType& t){
    type_ = t.type_->clone();
  }

  virtual ~AddressType () {
    if (type_) delete type_;
  }

  void print (ostream& os) const {
		os<<"Node name : AddressType"<<endl;
	  assert(type_);
	  type_->print(os);
  }

  Node * clone () const { return new AddressType(*this);}

public:
  Node * type_;
};


class Parameter : public Node {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  Parameter (Node * type, const char * name) : type_(type){
    assert(type_);
    name_ = new string(name);
  }

  Parameter(const Parameter& p){
    type_ = p.type_->clone();
    name_ = new string(*p.name_);
  }

  virtual ~Parameter () {
    if (type_) delete type_;
		delete name_;
	}

  void print (ostream& os) const {
		printWayOfPassing(os);
    os<<"Parameter name :" <<name_<<endl;
		assert(type_);
	  type_->print(os);
  }

protected:
	virtual void printWayOfPassing (ostream& os) const = 0;

public:
  Node * type_;
  string * name_;
};

class ByReferenceParameter : public Parameter {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
	ByReferenceParameter (Node * type, const char * name) : Parameter (type,name) {}
  Node * clone () const { return new ByReferenceParameter(*this);}
protected:
  void printWayOfPassing (ostream& os) const{
	  os<<"Node name : ByReferenceParameter ";
	}
};

class ByValueParameter : public Parameter {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
	ByValueParameter (Node * type, const char * name) : Parameter(type,name) {}
  Node * clone () const { return new ByValueParameter(*this);}
protected:
  void printWayOfPassing (ostream& os) const{
	  os<<"Node name : ByValueParameter ";
	}
};

class ParameterList : public Node {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  ParameterList (Node * formal) : formal_(formal),  formal_list_(NULL) { assert(formal_);}
  ParameterList (Node * formal, Node * formal_list) : formal_(formal), formal_list_(formal_list) {assert(formal_ && formal_list_);}

  ParameterList(const ParameterList& pl){
    formal_ = pl.formal_->clone();
    formal_list_ = pl.formal_list_->clone();
  }

  virtual ~ParameterList () {
    if (formal_) delete formal_;
    if (formal_list_) delete formal_list_;
	}

  void print (ostream& os) const {
	  os<<"Node name : ParameterList"<<endl;
	  if (formal_list_){
		  formal_list_->print(os);
	  }
	  assert(formal_);
	  formal_->print(os);
  }

  Node * clone () const { return new ParameterList(*this);}

public:
  Node * formal_;
  Node * formal_list_;
};

class FunctionDeclaration : public Declaration {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  FunctionDeclaration (Node * type, Node * block, const char * name) : type_(type), block_(block), formal_list_(NULL) {
    assert(type_ && block_);
    name_ = new string(name);
  }

  FunctionDeclaration (Node * type, Node * formal_list, Node * block,  const char * name) : type_(type), formal_list_(formal_list),block_(block) {
    assert(type_ && formal_list_ && block_ );
    name_ = new string(name);
  }

  virtual ~FunctionDeclaration () {
    if (type_) delete type_;
    if (block_) delete block_;
    if (formal_list_) delete formal_list_;
    if (name_) delete name_;
  }

  FunctionDeclaration(const FunctionDeclaration& fd){
    type_ = fd.type_->clone();
    block_ = fd.block_->clone();
    formal_list_ = fd.formal_list_->clone();
    name_ = new string(*fd.name_);
  }

  void print (ostream& os) const {
    os<<"Node name : FunctionDeclaration. Func name is: "<<*name_<<endl;
	  assert(type_ && block_);
	  type_->print(os);
	  if (formal_list_){
		  formal_list_->print(os);
	  }
	  block_->print(os);
  }

  Node * clone () const { return new FunctionDeclaration(*this);}

public:
  Node * type_;
  Node * block_;
  Node * formal_list_;
  string * name_;
};

class ProcedureDeclaration : public Declaration {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  ProcedureDeclaration (Node * block, const char * name) : formal_list_(NULL), block_(block) {
    assert(block_);
    name_ = new string(name);
  }

  ProcedureDeclaration (Node * formal_list, Node * block, const char * name) : formal_list_(formal_list),block_(block)  {
    assert(formal_list_ && block_);
    name_ = new string(name);
  }

  virtual ~ProcedureDeclaration () {
    if (block_) delete block_;
    if (formal_list_) delete formal_list_;
    if (name_) delete name_;
  }

  ProcedureDeclaration(const ProcedureDeclaration& pd){
    block_ = pd.block_->clone();
    formal_list_ = pd.formal_list_->clone();
    name_ = new string(*pd.name_);
  }

  void print (ostream& os) const {
	  os<<"Node name : ProcedureDeclaration. Proc name is: "<<*name_<<endl;
	  assert(block_);
	  if (formal_list_){
		  formal_list_->print(os);
	  }
	  block_->print(os);
  }

  Node * clone () const { return new ProcedureDeclaration(*this);}

public:
  Node * block_;
  Node * formal_list_;
  string * name_;
};

class DeclarationList : public Node {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  DeclarationList (Node * decl) : decl_(decl), decl_list_(NULL) { assert(decl_);}
  DeclarationList (Node * decl_list, Node * decl) : decl_list_(decl_list),decl_(decl)  {assert(decl_list_ && decl);}

  DeclarationList(const DeclarationList& dl){
    decl_ = dl.decl_->clone();
    decl_list_ = dl.decl_list_->clone();
  }

  virtual ~DeclarationList () {
    if (decl_) delete decl_;
    if (decl_list_) delete decl_list_;
  }

  void print (ostream& os) const {
	  os<<"Node name : DeclarationList"<<endl;
	  if (decl_list_){
		  decl_list_->print(os);
	  }
	  assert(decl_);
	  decl_->print(os);
  }

  Node * clone () const { return new DeclarationList(*this);}

public:
  Node * decl_;
  Node * decl_list_;
};

class Block : public Node {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  Block (Node * stat_seq) : stat_seq_(stat_seq), decl_list_(NULL)  { assert(stat_seq_);}
  Block (Node * decl_list, Node * stat_seq) : decl_list_(decl_list), stat_seq_(stat_seq)  {assert(decl_list_ && stat_seq_);}

  Block(const Block& b){
    decl_list_ = b.decl_list_->clone();
    stat_seq_ = b.stat_seq_->clone();
  }

  virtual ~Block () {
    if (stat_seq_) delete stat_seq_;
    if (decl_list_) delete decl_list_;
  }

  void print (ostream& os) const {
	  os<<"Node name : Begin"<<endl;
	  if (decl_list_){
		  decl_list_->print(os);
	  }
	  assert(stat_seq_);
	  stat_seq_->print(os);
  }

  Node * clone () const { return new Block(*this);}

public:
  Node * decl_list_;
  Node * stat_seq_;
};

class Program : public Node {
public :
	void accept(Visitor * v){
		v->visit(this);
	}
  Program (Node * block, const char * str) : block_(NULL) {
    block_ = dynamic_cast<Block *>(block);
    assert(block_);
    name_ = new string(str);
  }

  Program(const Program& prog){
    block_ = dynamic_cast<Block *>(prog.block_->clone());
    assert(block_);
    name_ = new string(*prog.name_);
  }

  virtual ~Program () {
    if (block_) delete block_;
    delete name_;
  }

  void print (ostream& os) const {
    os<<"Node name : Root/Program. Program name is: "<<*name_<<endl;
	  assert(block_);
    block_->print(os);
  }

  Node * clone () const { return new Program(*this);}

public:
  Block * block_;
  string * name_;
};





#endif //AST_H
