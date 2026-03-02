  { ---------------------------------------------------------- }
  {          unit: List2.pas  pointer implementation           }
  { ---------------------------------------------------------- }

  unit List2 ;

  (* ------------- *)
       INTERFACE
  (* ------------- *)

  uses wincrt;

  type
      list_info_type =
    record
        NAME: string[20];
        GRADE: integer;
    end;   {  can be any other known type }

      pos_type = ^link_type;       {  pointer to link_type  }

     {-----------------------------------------------}
     {          link type definition                 }
      link_type = record
                   info: list_info_type;
                   next: pos_type;
                end;

      {--------------------------------------------}
      {          anchor definition                 }
      list_type = record
                   anchor: pos_type;
                   (* more  fields  if  needed *)
                end;
  (* ----------------------------------------------------------------*)
  {-------------------------------------------}
  {      returns an initialized list          }
  procedure list_init ( var list : list_type ) ;

  {-------------------------------------------------}
  {       return  place:  anchor                    }
  {       assumption: L is initialized              }
  function list_anchor ( list : list_type ) : pos_type ;

  {--------------------------------------------------}
  {       return  place:  list_end		     }
  function list_end ( list : list_type ) : pos_type ;

  {-------------------------------------------------------}
  {    return  place:  next link in list		  }	
  {    assumption: L is initialized, pos is not list_end  }
  function list_next ( list : list_type ;
                        pos : pos_type ) : pos_type ;

  {---------------------------------------------------------}
  {   return  place:  previous link in list		    }	
  {   assumption: L is initialized,  pos is not list_anchor }
  function list_prev ( list : list_type ;
                        pos : pos_type ) : pos_type;

  {--------------------------------------------------------}
  {    inserting x after pos in L			   }  	
  {    assumption: L is initialized, pos is not list_end   }
  procedure list_insert ( var list : list_type ;
                               pos : pos_type ;
                                 x : list_info_type ) ;
  {----------------------------------------------------------------------}
  {   delets link in place pos in L					 }
  {   after deletation, pos points to next item before deletation	 }
  {   assumption: L is initialized, pos is neither anchor nor list-end.  }
  procedure list_delete ( var list : list_type ;
                          var  pos : pos_type ) ;

  {---------------------------------------------------------------------}
  {  returns data from place pos in L    				}
  {  assumption: L is initialized, pos is neither anchor nor list-end.  }
  procedure list_retrieve ( list : list_type ;
                             pos : pos_type ;
                           var x : list_info_type ) ;

  {---------------------------------------------------------------------}
  {   updats data in place pos in L        				}
  {   assumption: L is initialized, pos is neither anchor nor list-end. }
  procedure list_update ( var list : list_type ;
                               pos : pos_type ;
                                 x : list_info_type ) ;

  {-----------------------------------------------------------}
  {    return TRUE if list is empty, atherwise return FALSE   }
  {    assumption: L is initialized              	      }
  function list_is_empty ( list : list_type ) : boolean ;

  
  (* ------------------ *)
       IMPLEMENTATION
  (* ------------------ *)

  procedure list_init ( var list : list_type ) ;
  begin
     new(list.anchor);
     list.anchor^.next := nil
  end;

  function list_anchor ( list : list_type ) : pos_type ;
  begin
     list_anchor := list.anchor
  end;

  function list_end ( list : list_type ) : pos_type ;
  begin
     list_end := nil
  end;

  function list_next ( list : list_type ;
                        pos : pos_type ) : pos_type ;
  begin
     list_next := pos^.next
  end;

  function list_prev ( list : list_type ;
                        pos : pos_type ) : pos_type;
  var
     q: pos_type;
  begin
     q := list_anchor(list);
     while list_next(list,q) <> pos do
        q := list_next(list,q);
     list_prev := q
  end;

  procedure list_insert ( var list : list_type ;
                               pos : pos_type ;
                                 x : list_info_type ) ;
  var
     q: pos_type;
  begin
     new(q);
     q^.next := pos^.next;
     pos^.next := q;
     q^.info := x
  end;

  procedure list_delete ( var list : list_type ;
                          var  pos : pos_type ) ;
  var
     q: pos_type;
  begin
     q := list_prev(list,pos);
     q^.next := pos^.next;
     dispose(pos);
     pos := q^.next
  end;

  procedure list_retrieve ( list : list_type ;
                             pos : pos_type ;
                           var x : list_info_type ) ;
  begin
     x := pos^.info
  end;

  procedure list_update ( var list : list_type ;
                               pos : pos_type ;
                                 x : list_info_type ) ;
  begin
     pos^.info := x;
  end;

  function list_is_empty ( list : list_type ) : boolean ;
  begin
     list_is_empty := list.anchor^.next = NIL
  end;


  begin
  end.


