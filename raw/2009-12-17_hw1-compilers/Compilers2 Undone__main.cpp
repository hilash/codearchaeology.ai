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

#include "ast.h"
#include "main.h"
#include "codeVisitor.h"
#include "codeRVisitor.h"
#include "codeLVisitor.h"

#include "typedef.h"

/**
 * Global Node, root of AST created by the parser (look for instantiation at miny.tab.cpp)
 */
extern Node * root;
extern int yyparse (void);

extern "C" {
	// Pointer to the program file that should be compiled.
	extern FILE *yyin;
	// syntax check and AST builder.
  extern int yylex(void);
}

/**
 * Initialize global data structures
 */

Visitor* Visitor::code=NULL;
Visitor* Visitor::codeLeft=NULL;
Visitor* Visitor::codeRight=NULL;

void writeAST (Node * r, fstream& file)
{
	//Perform recursive tree print
  r->print(file);
}

Node * getTree(char *progFile)
{
	assert(progFile);

	// yyin is an external variable that been used in yyparse as pointer to the source file.
	yyin = fopen(progFile,"r");
	if (!yyin){
	   cerr<<"Error: file "<<progFile<<" does not exst. Aborting ..."<<endl;;
       exit(1);
    }
	assert(yyin);

	// yyparse is yacc function that parse the program, checks syntax and builds the program AST.
	yyparse();

  fclose(yyin);

	// root was initialized in yyparse while it was building the tree.
	// root is the pointer of the returning tree.
	assert(root);
	return(root);
}

int main(int argc, char* argv[] )
{
  fstream * OutputFile_ = new fstream(OUTPUT_CODE_TEXT,ios::out);

	Visitor::code= new CodeVisitor(*OutputFile_);
	Visitor::codeLeft=new CodeLVisitor(*OutputFile_);
	Visitor::codeRight=new CodeRVisitor(*OutputFile_);
	
  assert(OutputFile_);

  //check input arguments.
  if ( argc < 2 ){
    cerr<<endl<<"Input file is missing. Aborting ..."<<endl;
    exit(1);
  }

  Node * theProgram = getTree(argv[1]);
  assert(theProgram == root);

  fstream treeFile(TREE_OUTPUT_TEXT_FILE,ios::out);
  treeFile<<AST_FILE_HEADER<<endl;
  writeAST(theProgram,treeFile);
  treeFile.close();

	try{
    theProgram->accept(Visitor::code);
	}
	catch (exception e){
		cerr<<e.what()<<". Aborting ..."<<endl;
		exit(1);
	}

  //deallocate memory
  delete theProgram;
  OutputFile_->close();
	return (0);
}
