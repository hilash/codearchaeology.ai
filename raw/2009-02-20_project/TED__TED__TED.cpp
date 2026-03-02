#include "TED.h"

TED::TED(Tree* F,Tree* G)
{
	F->FindHeavyPath();		//framework, get trees sizes
	G->FindHeavyPath();		//framework, get trees sizes

	TED_pro(F,G);
}

void TED::TED_pro(Tree* F,Tree* G)
{
	if ( F->getSize() < G->getSize() )
	{
		TED_pro(G,F);
		return;
	}
	// get top light  & heavy path

	// computing period

}