grammar MapParser;

prog : (map NEWLINE?)+;
map : MAP fb1;
fb1 : FB datatype1;
datatype1 : dataType comma1;
comma1 : COMMA datatype2;
datatype2 : dataType lb1;
lb1 : LB name;
name : IDENTIFIER ravno;
ravno : EQUAL new;
new : NEW hashmap;
hashmap : HASHMAP fb2;
fb2 : FB datatype3;
datatype3 : dataType comma2;
comma2 : COMMA datatype4;
datatype4 : dataType lb2;
lb2 : LB lp;
lp : LP rp;
rp : RP semicolon;
semicolon : SEMICOLON;

dataType : 'Int' | 'String' | 'Bool' | 'Float' | 'Byte' | 'Short' | 'Long' | 'Boolean' | 'Char' ;

NEWLINE : [\r\n]+ ;
MAP: 'Map';
FB: '<' ;
LB: '>' ;
COMMA: ',' ;
EQUAL: '=' ;
NEW: 'new' ;
HASHMAP: 'HashMap' ;
LP: '(' ;
RP: ')' ;
SEMICOLON: ';' ;

IDENTIFIER : ('a'..'z'|'A'..'Z') ('a'..'z'|'A'..'Z')*;
WS : [ \t\r\n]+ -> skip ;
