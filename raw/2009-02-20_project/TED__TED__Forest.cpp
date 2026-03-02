#include "Forest.h"

void  Forest::PrintForest()
{
  for(int i = 0; i < Frst.size(); i++)
	  cout <<i << "  "<<  Frst[i]->getData()->getLabel() << endl;
}
