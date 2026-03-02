#include "Lse.h"


int main()
{
	// the tree at wikipedia tree traveral page
	//Tree A(new Node(1,0,0),NULL,NULL);
	//Tree C(new Node(3,0,0),NULL,NULL);
	//Tree E(new Node(5,0,0),NULL,NULL);
	//Tree H(new Node(7,0,0),NULL,NULL);
	//Tree I(new Node(8,0,0),&H,NULL);
	//Tree G(new Node(9,0,0),NULL,&I);
	//Tree D(new Node(4,0,0),&C,&E);
	//Tree B(new Node(2,0,0),&A,&D);	
	//Tree F(new Node(6,0,0),&B,&G);


	// the fk tree like at Oren's article
	Tree E(new Node(5,0,0),NULL,NULL);
	Tree I(new Node(9,0,0),NULL,NULL);
	Tree J(new Node(10,0,0),NULL,NULL);
	Tree K(new Node(11,0,0),NULL,NULL);
	Tree L(new Node(12,0,0),NULL,NULL);

	Tree H(new Node(8,0,0),&K,&L);
	Tree G(new Node(7,0,0),&I,&J);
	Tree F(new Node(6,0,0),&G,&H);

	Tree C(new Node(3,0,0),&E,NULL);
	Tree D(new Node(4,0,0),NULL,NULL);
	Tree B(new Node(2,0,0),&C,&D);
	Tree A(new Node(1,0,0),&B,&F);

	A.FindHeavyPath();

	//F.PrintHeavyPath();
	//cout << "\n";
	//F.PrintTopLight();
	//cout << "\n";

	//A.PrintTree();

	Lse kk(&A);
	kk.PrintW();
	//kk.getWi(1)->getData()->PrintNode();
	cout << "fff";

	//kk.PrintW();

	//getWi(i)->getSize();
	//for (int i=0; i<=F.getSize(); i++)
	//cout <<i << " "<< kk.j(i) << endl;


	//Ilse N;
	//N.find_Ilse(&A,&F,LEFT);
	//Forest* NN = N.get_Ilse();
	//NN->PrintForest();
	//cout << endl;
	//N.get_Fk(3)->PrintForest();


	//Ilse FF;
	//FF.ILSE(&F,&A);

	//Fr->PrintForest();



	
	// postorder results
	//A, C, E, D, B, H, I, G, F
	//1, 3, 5, 4, 2, 7, 8, 9, 6

	return 0;
}