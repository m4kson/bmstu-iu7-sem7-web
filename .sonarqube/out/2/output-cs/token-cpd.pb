Ïf
w/Users/m4ks0n/study/IU7/sem7/testing/bmstu-iu7-sem7-web/src/ProdMonitor/ProdMonitor.Application/Services/UserService.cs
	namespace		 	
ProdMonitor		
 
.		 
Application		 !
.		! "
Services		" *
{

 
public 

class 
UserService 
: 
IUserService +
{ 
private 
readonly 
IUserRepository (
_userRepository) 8
;8 9
private 
readonly 
ILogger  
_logger! (
;( )
public 
UserService 
( 
IUserRepository *
userRepository+ 9
,9 :
ILogger 
logger 
) 
{ 	
_userRepository 
= 
userRepository ,
;, -
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
List 
< 
User #
># $
>$ %
GetAllUsersAsync& 6
(6 7

UserFilter7 A
filterB H
)H I
{ 	
_logger 
. 
Information 
(  
$str  ]
)] ^
;^ _
try 
{ 
var 
users 
= 
await !
_userRepository" 1
.1 2
GetAllUsersAsync2 B
(B C
filterC I
)I J
;J K
if 
( 
! 
users 
. 
Any 
( 
)  
)  !
{ 
_logger 
. 
Warning #
(# $
$str$ O
)O P
;P Q
throw   
new   !
UserNotFoundException   3
(  3 4
$str  4 _
)  _ `
;  ` a
}!! 
_logger"" 
."" 
Information"" #
(""# $
$str""$ O
,""O P
users""Q V
.""V W
Count""W \
)""\ ]
;""] ^
return## 
users## 
;## 
}$$ 
catch%% 
(%% !
UserNotFoundException%% (
)%%( )
{&& 
throw'' 
;'' 
}(( 
catch)) 
()) 
	Exception)) 
ex)) 
)))  
{** 
_logger++ 
.++ 
Error++ 
(++ 
ex++  
,++  !
$str++" =
)++= >
;++> ?
throw,, 
new,,  
UserServiceException,, .
(,,. /
$str,,/ D
,,,D E
ex,,F H
),,H I
;,,I J
}-- 
}.. 	
public00 
async00 
Task00 
<00 
User00 
>00 
GetUserById00  +
(00+ ,
Guid00, 0
id001 3
)003 4
{11 	
_logger22 
.22 
Information22 
(22  
$str22  O
,22O P
id22Q S
)22S T
;22T U
try33 
{44 
var55 
user55 
=55 
await55  
_userRepository55! 0
.550 1
GetUserByIdAsync551 A
(55A B
id55B D
)55D E
;55E F
if66 
(66 
user66 
==66 
null66  
)66  !
{77 
_logger88 
.88 
Warning88 #
(88# $
$str88$ F
,88F G
id88H J
)88J K
;88K L
throw99 
new99 !
UserNotFoundException99 3
(993 4
$"994 6
$str996 C
{99C D
id99D F
}99F G
$str99G Q
"99Q R
)99R S
;99S T
}:: 
_logger;; 
.;; 
Information;; #
(;;# $
$str;;$ S
,;;S T
id;;U W
);;W X
;;;X Y
return<< 
user<< 
;<< 
}== 
catch>> 
(>> !
UserNotFoundException>> (
)>>( )
{?? 
throw@@ 
;@@ 
}AA 
catchBB 
(BB 
	ExceptionBB 
exBB 
)BB  
{CC 
_loggerDD 
.DD 
ErrorDD 
(DD 
exDD  
,DD  !
$strDD" M
,DDM N
idDDO Q
)DDQ R
;DDR S
throwEE 
newEE  
UserServiceExceptionEE .
(EE. /
$strEE/ C
,EEC D
exEEE G
)EEG H
;EEH I
}FF 
}GG 	
publicII 
asyncII 
TaskII 
<II 
UserII 
>II 
UpdateUserRoleII  .
(II. /
GuidII/ 3
userIdII4 :
,II: ;
UserUpdateRoleII< J
roleIIK O
)IIO P
{JJ 	
_loggerKK 
.KK 
InformationKK 
(KK  
$strKK  `
,KK` a
userIdKKb h
,KKh i
roleKKj n
.KKn o
RoleKKo s
)KKs t
;KKt u
tryLL 
{MM 
varNN 
updatedUserNN 
=NN  !
awaitNN" '
_userRepositoryNN( 7
.NN7 8
UpdateUserRoleAsyncNN8 K
(NNK L
userIdNNL R
,NNR S
roleNNT X
)NNX Y
;NNY Z
_loggerOO 
.OO 
InformationOO #
(OO# $
$strOO$ d
,OOd e
userIdOOf l
,OOl m
roleOOn r
.OOr s
RoleOOs w
)OOw x
;OOx y
returnPP 
updatedUserPP "
;PP" #
}QQ 
catchRR 
(RR !
UserNotFoundExceptionRR (
)RR( )
{SS 
throwTT 
;TT 
}UU 
catchVV 
(VV 
	ExceptionVV 
exVV 
)VV  
{WW 
_loggerXX 
.XX 
ErrorXX 
(XX 
exXX  
,XX  !
$strXX" T
,XXT U
userIdXXV \
)XX\ ]
;XX] ^
throwYY 
newYY  
UserServiceExceptionYY .
(YY. /
$strYY/ N
,YYN O
exYYP R
)YYR S
;YYS T
}ZZ 
}[[ 	
public]] 
async]] 
Task]] 
<]] 
User]] 
>]] 

UpdateUser]]  *
(]]* +
Guid]]+ /
userId]]0 6
,]]6 7
RegisterModel]]8 E
userData]]F N
)]]N O
{^^ 	
try__ 
{`` 
varbb 
userbb 
=bb 
awaitbb  
_userRepositorybb! 0
.bb0 1
GetUserByIdAsyncbb1 A
(bbA B
userIdbbB H
)bbH I
;bbI J
ifcc 
(cc 
usercc 
==cc 
nullcc  
)cc  !
{dd 
_loggeree 
.ee 
Warningee #
(ee# $
$stree$ F
,eeF G
userIdeeH N
)eeN O
;eeO P
throwff 
newff !
UserNotFoundExceptionff 3
(ff3 4
$"ff4 6
$strff6 C
{ffC D
userIdffD J
}ffJ K
$strffK U
"ffU V
)ffV W
;ffW X
}gg 
byteii 
[ii 
]ii 
passwordHashii #
,ii# $
passwordSaltii% 1
;ii1 2
CreatePasswordHashjj "
(jj" #
userDatajj# +
.jj+ ,
Passwordjj, 4
,jj4 5
outjj6 9
passwordHashjj: F
,jjF G
outjjH K
passwordSaltjjL X
)jjX Y
;jjY Z
varll 
newUserll 
=ll 
newll !

UserCreatell" ,
(ll, -
namemm 
:mm 
userDatamm "
.mm" #
Namemm# '
,mm' (
surnamenn 
:nn 
userDatann %
.nn% &
Surnamenn& -
,nn- .

patronymicoo 
:oo 
userDataoo  (
.oo( )

Patronymicoo) 3
,oo3 4

departmentpp 
:pp 
userDatapp  (
.pp( )

Departmentpp) 3
,pp3 4
emailqq 
:qq 
userDataqq #
.qq# $
Emailqq$ )
,qq) *
passwordHashrr  
:rr  !
passwordHashrr" .
,rr. /
passwordSaltss  
:ss  !
passwordSaltss" .
,ss. /
birthDaytt 
:tt 
userDatatt &
.tt& '
BirthDaytt' /
,tt/ 0
sexuu 
:uu 
userDatauu !
.uu! "
Sexuu" %
,uu% &
rolevv 
:vv 
uservv 
.vv 
Rolevv #
,vv# $
twoFactorCodeww !
:ww! "
userww# '
.ww' (
TwoFactorCodeww( 5
,ww5 6
twoFactorExpirationxx '
:xx' (
userxx) -
.xx- .
TwoFactorExpirationxx. A
)xxA B
;xxB C
varzz 
updatedUserzz 
=zz  !
awaitzz" '
_userRepositoryzz( 7
.zz7 8
UpdateUserAsynczz8 G
(zzG H
userIdzzH N
,zzN O
newUserzzP W
)zzW X
;zzX Y
return{{ 
updatedUser{{ "
;{{" #
}|| 
catch}} 
(}} !
UserNotFoundException}} (
)}}( )
{~~ 
throw 
; 
}
ÄÄ 
catch
ÅÅ 
(
ÅÅ 
	Exception
ÅÅ 
ex
ÅÅ 
)
ÅÅ  
{
ÇÇ 
_logger
ÉÉ 
.
ÉÉ 
Error
ÉÉ 
(
ÉÉ 
ex
ÉÉ  
,
ÉÉ  !
$str
ÉÉ" K
,
ÉÉK L
userId
ÉÉM S
)
ÉÉS T
;
ÉÉT U
throw
ÑÑ 
new
ÑÑ "
UserServiceException
ÑÑ .
(
ÑÑ. /
$str
ÑÑ/ K
,
ÑÑK L
ex
ÑÑM O
)
ÑÑO P
;
ÑÑP Q
}
ÖÖ 
}
ÜÜ 	
public
àà 
async
àà 
Task
àà 
DeleteUserAsync
àà )
(
àà) *
Guid
àà* .
userId
àà/ 5
)
àà5 6
{
ââ 	
_logger
ää 
.
ää 
Information
ää 
(
ää  
$str
ää  M
,
ääM N
userId
ääO U
)
ääU V
;
ääV W
try
ãã 
{
åå 
await
çç 
_userRepository
çç %
.
çç% &
DeleteUserAsync
çç& 5
(
çç5 6
userId
çç6 <
)
çç< =
;
çç= >
_logger
éé 
.
éé 
Information
éé #
(
éé# $
$str
éé$ Q
,
ééQ R
userId
ééS Y
)
ééY Z
;
ééZ [
}
èè 
catch
êê 
(
êê #
UserNotFoundException
êê (
)
êê( )
{
ëë 
throw
íí 
;
íí 
}
ìì 
catch
îî 
(
îî 
	Exception
îî 
ex
îî 
)
îî  
{
ïï 
_logger
ññ 
.
ññ 
Error
ññ 
(
ññ 
ex
ññ  
,
ññ  !
$str
ññ" K
,
ññK L
userId
ññM S
)
ññS T
;
ññT U
throw
óó 
new
óó "
UserServiceException
óó .
(
óó. /
$str
óó/ F
,
óóF G
ex
óóH J
)
óóJ K
;
óóK L
}
òò 
}
ôô 	
public
õõ 
void
õõ  
CreatePasswordHash
õõ &
(
õõ& '
string
õõ' -
password
õõ. 6
,
õõ6 7
out
õõ8 ;
byte
õõ< @
[
õõ@ A
]
õõA B
passwordHash
õõC O
,
õõO P
out
õõQ T
byte
õõU Y
[
õõY Z
]
õõZ [
passwordSalt
õõ\ h
)
õõh i
{
úú 	
using
ùù 
(
ùù 
var
ùù 
hmac
ùù 
=
ùù 
new
ùù !
System
ùù" (
.
ùù( )
Security
ùù) 1
.
ùù1 2
Cryptography
ùù2 >
.
ùù> ?

HMACSHA512
ùù? I
(
ùùI J
)
ùùJ K
)
ùùK L
{
ûû 
passwordSalt
üü 
=
üü 
hmac
üü #
.
üü# $
Key
üü$ '
;
üü' (
passwordHash
†† 
=
†† 
hmac
†† #
.
††# $
ComputeHash
††$ /
(
††/ 0
System
††0 6
.
††6 7
Text
††7 ;
.
††; <
Encoding
††< D
.
††D E
UTF8
††E I
.
††I J
GetBytes
††J R
(
††R S
password
††S [
)
††[ \
)
††\ ]
;
††] ^
}
°° 
}
¢¢ 	
}
££ 
}§§ ∫;
z/Users/m4ks0n/study/IU7/sem7/testing/bmstu-iu7-sem7-web/src/ProdMonitor/ProdMonitor.Application/Services/TractorService.cs
	namespace 	
ProdMonitor
 
. 
Application !
.! "
Services" *
{ 
public		 

class		 
TractorService		 
:		  !
ITractorService		" 1
{

 
private 
readonly 
ITractorRepository +
_tractorRepository, >
;> ?
private 
readonly 
ILogger  
_logger! (
;( )
public 
TractorService 
( 
ITractorRepository 0
tractorRepository1 B
,B C
ILogger 
logger 
) 
{ 	
_tractorRepository 
=  
tractorRepository! 2
;2 3
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Tractor !
>! "
CreateTractorAsync# 5
(5 6
TractorCreate6 C
tractorD K
)K L
{ 	
_logger 
. 
Information 
(  
$str  S
,S T
tractorU \
.\ ]
Model] b
)b c
;c d
try 
{ 
var 
createdTractor "
=# $
await% *
_tractorRepository+ =
.= >
CreateTractorAsync> P
(P Q
tractorQ X
)X Y
;Y Z
_logger 
. 
Information #
(# $
$str$ V
,V W
createdTractorX f
.f g
Idg i
)i j
;j k
return 
createdTractor %
;% &
} 
catch 
( 
	Exception 
ex 
)  
{ 
_logger   
.   
Error   
(   
ex    
,    !
$str  " K
,  K L
tractor  M T
.  T U
Model  U Z
)  Z [
;  [ \
throw!! 
new!! #
TractorServiceException!! 1
(!!1 2
$str!!2 L
,!!L M
ex!!N P
)!!P Q
;!!Q R
}"" 
}$$ 	
public&& 
async&& 
Task&& 
<&& 
List&& 
<&& 
Tractor&& &
>&&& '
>&&' (
GetAllTractorsAsync&&) <
(&&< =
TractorFilter&&= J
filter&&K Q
)&&Q R
{'' 	
_logger(( 
.(( 
Information(( 
(((  
$str((  _
)((_ `
;((` a
try)) 
{** 
var++ 
tractors++ 
=++ 
await++ $
_tractorRepository++% 7
.++7 8
GetAllTractorsAsync++8 K
(++K L
filter++L R
)++R S
;++S T
if,, 
(,, 
!,, 
tractors,, 
.,, 
Any,, !
(,,! "
),," #
),,# $
{-- 
_logger.. 
... 
Warning.. #
(..# $
$str..$ Q
)..Q R
;..R S
throw// 
new// $
TractorNotFoundException// 6
(//6 7
$str//7 d
)//d e
;//e f
}00 
_logger11 
.11 
Information11 #
(11# $
$str11$ T
,11T U
tractors11V ^
.11^ _
Count11_ d
)11d e
;11e f
return22 
tractors22 
;22  
}33 
catch44 
(44 $
TractorNotFoundException44 +
)44+ ,
{55 
throw66 
;66 
}77 
catch88 
(88 
	Exception88 
ex88 
)88  
{99 
_logger:: 
.:: 
Error:: 
(:: 
ex::  
,::  !
$str::" ?
)::? @
;::@ A
throw;; 
new;; #
TractorServiceException;; 1
(;;1 2
$str;;2 J
,;;J K
ex;;L N
);;N O
;;;O P
}<< 
}== 	
public?? 
async?? 
Task?? 
<?? 
Tractor?? !
>??! "
GetTractorByIdAsync??# 6
(??6 7
Guid??7 ;
id??< >
)??> ?
{@@ 	
_loggerAA 
.AA 
InformationAA 
(AA  
$strAA  T
,AAT U
idAAV X
)AAX Y
;AAY Z
tryBB 
{CC 
varDD 
tractorDD 
=DD 
awaitDD #
_tractorRepositoryDD$ 6
.DD6 7
GetTractorByIdAsyncDD7 J
(DDJ K
idDDK M
)DDM N
;DDN O
ifEE 
(EE 
tractorEE 
==EE 
nullEE #
)EE# $
{FF 
_loggerGG 
.GG 
WarningGG #
(GG# $
$strGG$ K
,GGK L
idGGM O
)GGO P
;GGP Q
throwHH 
newHH $
TractorNotFoundExceptionHH 6
(HH6 7
$"HH7 9
$strHH9 I
{HHI J
idHHJ L
}HHL M
$strHHM W
"HHW X
)HHX Y
;HHY Z
}II 
_loggerJJ 
.JJ 
InformationJJ #
(JJ# $
$strJJ$ X
,JJX Y
idJJZ \
)JJ\ ]
;JJ] ^
returnKK 
tractorKK 
;KK 
}LL 
catchMM 
(MM $
TractorNotFoundExceptionMM +
)MM+ ,
{NN 
throwOO 
;OO 
}PP 
catchQQ 
(QQ 
	ExceptionQQ 
exQQ 
)QQ  
{RR 
_loggerSS 
.SS 
ErrorSS 
(SS 
exSS  
,SS  !
$strSS" R
,SSR S
idSST V
)SSV W
;SSW X
throwTT 
newTT #
TractorServiceExceptionTT 1
(TT1 2
$strTT2 I
,TTI J
exTTK M
)TTM N
;TTN O
}UU 
}VV 	
publicXX 
asyncXX 
TaskXX 
DeleteTractorAsyncXX ,
(XX, -
GuidXX- 1
idXX2 4
)XX4 5
{YY 	
tryZZ 
{[[ 
_logger\\ 
.\\ 
Information\\ #
(\\# $
$str\\$ V
,\\V W
id\\X Z
)\\Z [
;\\[ \
await]] 
_tractorRepository]] (
.]]( )
DeleteTractorAsync]]) ;
(]]; <
id]]< >
)]]> ?
;]]? @
}^^ 
catch__ 
(__ $
TractorNotFoundException__ +
)__+ ,
{`` 
throwaa 
;aa 
}bb 
catchcc 
(cc 
	Exceptioncc 
excc 
)cc  
{dd 
_loggeree 
.ee 
Erroree 
(ee 
exee  
,ee  !
$stree" P
,eeP Q
ideeR T
)eeT U
;eeU V
throwff 
newff #
TractorServiceExceptionff 1
(ff1 2
$strff2 L
,ffL M
exffN P
)ffP Q
;ffQ R
}gg 
}hh 	
}ii 
}jj ü
y/Users/m4ks0n/study/IU7/sem7/testing/bmstu-iu7-sem7-web/src/ProdMonitor/ProdMonitor.Application/Services/SupplyService.cs
	namespace 	
ProdMonitor
 
. 
Application !
.! "
Services" *
{ 
public 

class 
SupplyService 
:  
ISupplyService! /
{ 
private 
readonly 
IDetailRepository *
_detailRepository+ <
;< =
private 
readonly 
ISupplyRepository *
_supplyRepository+ <
;< =
private 
readonly 
ILogger  
_logger! (
;( )
public 
SupplyService 
( 
IDetailRepository .
detailRepository/ ?
,? @
ISupplyRepository 
supplyRepository .
,. /
ILogger 
logger 
) 
{ 	
_detailRepository 
= 
detailRepository  0
;0 1
_supplyRepository 
= 
supplyRepository  0
;0 1
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Supply  
>  !
CreateSupplyAsync" 3
(3 4
SupplyCreate4 @
supplyCreateA M
)M N
{ 	
_logger 
. 
Information 
(  
$str  C
)C D
;D E
try 
{   
var!! 
detail!! 
=!! 
await!! "
_detailRepository!!# 4
.!!4 5
GetDetailByIdAsync!!5 G
(!!G H
supplyCreate!!H T
.!!T U
DetailId!!U ]
)!!] ^
;!!^ _
if"" 
("" 
detail"" 
=="" 
null"" "
)""" #
throw## 
new## 
	Exception## '
(##' (
$str##( :
)##: ;
;##; <
detail%% 
.%% 
Amount%% 
+=%%  
supplyCreate%%! -
.%%- .
Quantity%%. 6
;%%6 7
await&& 
_detailRepository&& '
.&&' (
UpdateDetailAsync&&( 9
(&&9 :
detail&&: @
.&&@ A
Id&&A C
,&&C D
detail&&E K
.&&K L
Amount&&L R
)&&R S
;&&S T
var)) 
	newSupply)) 
=)) 
await))  %
_supplyRepository))& 7
.))7 8
CreateSupplyAsync))8 I
())I J
supplyCreate))J V
)))V W
;))W X
_logger++ 
.++ 
Information++ #
(++# $
$str++$ Z
,++Z [
	newSupply++\ e
.++e f
Id++f h
)++h i
;++i j
return,, 
	newSupply,,  
;,,  !
}-- 
catch.. 
(.. 
	Exception.. 
ex.. 
)..  
{// 
throw00 
new00 
	Exception00 #
(00# $
$str00$ =
,00= >
ex00? A
)00A B
;00B C
}11 
}33 	
}44 
}55 ¥D
Å/Users/m4ks0n/study/IU7/sem7/testing/bmstu-iu7-sem7-web/src/ProdMonitor/ProdMonitor.Application/Services/ServiceRequestService.cs
	namespace 	
ProdMonitor
 
. 
Application !
.! "
Services" *
{		 
public

 

class

 !
ServiceRequestService

 &
:

' ("
IServiceRequestService

) ?
{ 
private 
readonly %
IServiceRequestRepository 2
_requestRepository3 E
;E F
private 
readonly #
IAssemblyLineRepository 0#
_assemblyLineRepository1 H
;H I
private 
readonly 
ILogger  
_logger! (
;( )
public !
ServiceRequestService $
($ %%
IServiceRequestRepository% >

repository? I
,I J#
IAssemblyLineRepository #
lineRepository$ 2
,2 3
ILogger 
logger 
) 
{ 	
_requestRepository 
=  

repository! +
;+ ,#
_assemblyLineRepository #
=$ %
lineRepository& 4
;4 5
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
ServiceRequest (
>( )%
CreateServiceRequestAsync* C
(C D 
ServiceRequestCreateD X
serviceRequestY g
)g h
{ 	
_logger 
. 
Information 
(  
$str  ]
,] ^
serviceRequest_ m
.m n
LineIdn t
)t u
;u v
var 
line 
= 
await #
_assemblyLineRepository 4
.4 5$
GetAssemblyLineByIdAsync5 M
(M N
serviceRequestN \
.\ ]
LineId] c
)c d
;d e
if 
( 
line 
== 
null 
) 
{ 
_logger   
.   
Warning   
(    
$str    J
,  J K
serviceRequest  L Z
.  Z [
LineId  [ a
)  a b
;  b c
throw!! 
new!! #
RequestServiceException!! 1
(!!1 2
$"!!2 4
$str!!4 ]
{!!] ^
serviceRequest!!^ l
.!!l m
LineId!!m s
}!!s t
"!!t u
)!!u v
;!!v w
}"" 
try## 
{$$ 
var%% 
createdReqest%% !
=%%" #
await%%$ )
_requestRepository%%* <
.%%< =%
CreateServiceRequestAsync%%= V
(%%V W
serviceRequest%%W e
.%%e f
LineId%%f l
,%%l m
serviceRequest&& "
.&&" #
UserId&&# )
,&&) *
DateTime'' 
.'' 
Now''  
.''  !
ToUniversalTime''! 0
(''0 1
)''1 2
,''2 3
RequestStatusType(( %
.((% &
Opened((& ,
,((, -
serviceRequest)) "
.))" #
Type))# '
,))' (
serviceRequest** "
.**" #
Description**# .
)**. /
;**/ 0
await,, #
_assemblyLineRepository,, -
.,,- .#
UpdateAssemblyLineAsync,,. E
(,,E F
serviceRequest,,F T
.,,T U
LineId,,U [
,,,[ \
new,,] `
AssemblyLineUpdate,,a s
(,,s t
status,,t z
:,,z {
LineStatusType	,,| ä
.
,,ä ã
	OnService
,,ã î
)
,,î ï
)
,,ï ñ
;
,,ñ ó
_logger-- 
.-- 
Information-- #
(--# $
$str--$ {
,--{ |
serviceRequest	--} ã
.
--ã å
LineId
--å í
,
--í ì
createdReqest
--î °
.
--° ¢
Id
--¢ §
)
--§ •
;
--• ¶
return.. 
createdReqest.. $
;..$ %
}// 
catch00 
(00 
	Exception00 
ex00 
)00  
{11 
_logger22 
.22 
Error22 
(22 
ex22  
,22  !
$str22" Y
,22Y Z
serviceRequest22[ i
.22i j
LineId22j p
)22p q
;22q r
throw33 
new33 #
RequestServiceException33 1
(331 2
$str332 L
,33L M
ex33N P
)33P Q
;33Q R
}44 
}55 	
public77 
async77 
Task77 
<77 
List77 
<77 
ServiceRequest77 -
>77- .
>77. /&
GetAllServiceRequestsAsync770 J
(77J K 
ServiceRequestFilter77K _
filter77` f
)77f g
{88 	
_logger99 
.99 
Information99 
(99  
$str99  g
)99g h
;99h i
try:: 
{;; 
var<< 
requests<< 
=<< 
await<< $
_requestRepository<<% 7
.<<7 8&
GetAllServiceRequestsAsync<<8 R
(<<R S
filter<<S Y
)<<Y Z
;<<Z [
if== 
(== 
!== 
requests== 
.== 
Any== !
(==! "
)==" #
)==# $
{>> 
_logger?? 
.?? 
Warning?? #
(??# $
$str??$ ?
)??? @
;??@ A
throw@@ 
new@@ $
RequestNotFoundException@@ 6
(@@6 7
$str@@7 J
)@@J K
;@@K L
}AA 
_loggerCC 
.CC 
InformationCC #
(CC# $
$strCC$ \
,CC\ ]
requestsCC^ f
.CCf g
CountCCg l
)CCl m
;CCm n
returnDD 
requestsDD 
;DD  
}EE 
catchFF 
(FF $
RequestNotFoundExceptionFF +
)FF+ ,
{GG 
throwHH 
;HH 
}II 
catchJJ 
(JJ 
	ExceptionJJ 
exJJ 
)JJ  
{KK 
_loggerLL 
.LL 
ErrorLL 
(LL 
exLL  
,LL  !
$strLL" G
)LLG H
;LLH I
throwMM 
newMM #
RequestServiceExceptionMM 1
(MM1 2
$strMM2 J
,MMJ K
exMML N
)MMN O
;MMO P
}NN 
}OO 	
publicQQ 
asyncQQ 
TaskQQ 
<QQ 
ServiceRequestQQ (
>QQ( )&
GetServiceRequestByIdAsyncQQ* D
(QQD E
GuidQQE I
idQQJ L
)QQL M
{RR 	
_loggerSS 
.SS 
InformationSS 
(SS  
$strSS  \
,SS\ ]
idSS^ `
)SS` a
;SSa b
tryUU 
{VV 
varWW 
requestWW 
=WW 
awaitWW #
_requestRepositoryWW$ 6
.WW6 7&
GetServiceRequestByIdAsyncWW7 Q
(WWQ R
idWWR T
)WWT U
;WWU V
ifXX 
(XX 
requestXX 
==XX 
nullXX #
)XX# $
{YY 
_loggerZZ 
.ZZ 
WarningZZ #
(ZZ# $
$strZZ$ S
,ZZS T
idZZU W
)ZZW X
;ZZX Y
throw[[ 
new[[ $
RequestNotFoundException[[ 6
([[6 7
$"[[7 9
$str[[9 I
{[[I J
id[[J L
}[[L M
$str[[M W
"[[W X
)[[X Y
;[[Y Z
}\\ 
_logger]] 
.]] 
Information]] #
(]]# $
$str]]$ `
,]]` a
id]]b d
)]]d e
;]]e f
return^^ 
request^^ 
;^^ 
}__ 
catch`` 
(`` $
RequestNotFoundException`` +
)``+ ,
{aa 
throwbb 
;bb 
}cc 
catchdd 
(dd 
	Exceptiondd 
exdd 
)dd  
{ee 
_loggerff 
.ff 
Errorff 
(ff 
exff  
,ff  !
$strff" Z
,ffZ [
idff\ ^
)ff^ _
;ff_ `
throwgg 
newgg #
RequestServiceExceptiongg 1
(gg1 2
$strgg2 I
,ggI J
exggK M
)ggM N
;ggN O
}hh 
}ii 	
}jj 
}kk ﬂè
Ä/Users/m4ks0n/study/IU7/sem7/testing/bmstu-iu7-sem7-web/src/ProdMonitor/ProdMonitor.Application/Services/ServiceReportService.cs
	namespace

 	
ProdMonitor


 
.

 
Application

 !
.

! "
Services

" *
{ 
public 

class  
ServiceReportService %
:& '!
IServiceReportService( =
{ 
private 
readonly $
IServiceReportRepository 1$
_serviceReportRepository2 J
;J K
private 
readonly %
IServiceRequestRepository 2%
_serviceRequestRepository3 L
;L M
private 
readonly #
IAssemblyLineRepository 0#
_assemblyLineRepository1 H
;H I
private 
readonly 
IUserRepository (
_userRepository) 8
;8 9
private 
readonly 
ILogger  
_logger! (
;( )
public  
ServiceReportService #
(# $$
IServiceReportRepository$ <#
serviceReportRepository= T
,T U%
IServiceRequestRepository %$
serviceRequestRepository& >
,> ?#
IAssemblyLineRepository #"
assemblyLineRepository$ :
,: ;
IUserRepository 
userRepository *
,* +
ILogger 
logger 
) 
{ 	$
_serviceReportRepository $
=% &#
serviceReportRepository' >
;> ?%
_serviceRequestRepository %
=& '$
serviceRequestRepository( @
;@ A#
_assemblyLineRepository #
=$ %"
assemblyLineRepository& <
;< =
_userRepository 
= 
userRepository ,
;, -
_logger 
= 
logger 
; 
} 	
public!! 
async!! 
Task!! 
<!! 
ServiceReport!! '
>!!' ($
CreateServiceReportAsync!!) A
(!!A B
ServiceReportCreate!!B U
report!!V \
)!!\ ]
{"" 	
_logger## 
.## 
Information## 
(##  
$str##  b
,##b c
report##d j
.##j k
	RequestId##k t
)##t u
;##u v
try$$ 
{%% 
var&& 
request&& 
=&& 
await&& #%
_serviceRequestRepository&&$ =
.&&= >&
GetServiceRequestByIdAsync&&> X
(&&X Y
report&&Y _
.&&_ `
	RequestId&&` i
)&&i j
;&&j k
if'' 
('' 
request'' 
=='' 
null'' #
)''# $
{(( 
_logger)) 
.)) 
Warning)) #
())# $
$str))$ K
,))K L
report))M S
.))S T
	RequestId))T ]
)))] ^
;))^ _
throw** 
new** 
ArgumentException** /
(**/ 0
$"**0 2
$str**2 B
{**B C
report**C I
.**I J
	RequestId**J S
}**S T
$str**T ^
"**^ _
)**_ `
;**` a
}++ 
if-- 
(-- 
request-- 
.-- 
Status-- "
!=--# %
RequestStatusType--& 7
.--7 8
Opened--8 >
)--> ?
{.. 
_logger// 
.// 
Warning// #
(//# $
$str//$ O
,//O P
report//Q W
.//W X
	RequestId//X a
)//a b
;//b c
throw00 
new00 
ArgumentException00 /
(00/ 0
$"000 2
$str002 B
{00B C
report00C I
.00I J
	RequestId00J S
}00S T
$str00T _
"00_ `
)00` a
;00a b
}11 
var44 
line44 
=44 
await44  #
_assemblyLineRepository44! 8
.448 9$
GetAssemblyLineByIdAsync449 Q
(44Q R
request44R Y
.44Y Z
LineId44Z `
)44` a
;44a b
if55 
(55 
line55 
==55 
null55  
)55  !
{66 
_logger77 
.77 
Warning77 #
(77# $
$str77$ N
,77N O
request77P W
.77W X
LineId77X ^
)77^ _
;77_ `
throw88 
new88 
ArgumentException88 /
(88/ 0
$"880 2
$str882 H
{88H I
request88I P
.88P Q
LineId88Q W
}88W X
$str88X b
"88b c
)88c d
;88d e
}99 
var;; 
user;; 
=;; 
await;;  
_userRepository;;! 0
.;;0 1
GetUserByIdAsync;;1 A
(;;A B
report;;B H
.;;H I
UserId;;I O
);;O P
;;;P Q
if<< 
(<< 
user<< 
==<< 
null<<  
)<<  !
{== 
_logger>> 
.>> 
Warning>> #
(>># $
$str>>$ E
,>>E F
report>>G M
.>>M N
UserId>>N T
)>>T U
;>>U V
throw?? 
new?? 
ArgumentException?? /
(??/ 0
$"??0 2
$str??2 ?
{??? @
report??@ F
.??F G
UserId??G M
}??M N
$str??N X
"??X Y
)??Y Z
;??Z [
}@@ 
varBB 
createdReportBB !
=BB" #
awaitBB$ )$
_serviceReportRepositoryBB* B
.BBB C$
CreateServiceReportAsyncBBC [
(BB[ \
requestBB\ c
.BBc d
LineIdBBd j
,BBj k
reportCC 
.CC 
UserIdCC !
,CC! "
reportDD 
.DD 
	RequestIdDD $
,DD$ %
DateTimeEE 
.EE 
NowEE  
.EE  !
ToUniversalTimeEE! 0
(EE0 1
)EE1 2
)EE2 3
;EE3 4
_loggerGG 
.GG 
InformationGG #
(GG# $
$strGG$ \
,GG\ ]
createdReportGG^ k
.GGk l
IdGGl n
)GGn o
;GGo p
awaitII %
_serviceRequestRepositoryII /
.II/ 0%
UpdateServiceRequestAsyncII0 I
(III J
createdReportIIJ W
.IIW X
	RequestIdIIX a
,IIa b
newIIc f 
ServiceRequestUpdateIIg {
(II{ |
status	II| Ç
:
IIÇ É
RequestStatusType
IIÑ ï
.
IIï ñ

InProgress
IIñ †
)
II† °
)
II° ¢
;
II¢ £
_loggerKK 
.KK 
InformationKK #
(KK# $
$strKK$ R
,KKR S
createdReportKKT a
.KKa b
	RequestIdKKb k
)KKk l
;KKl m
returnMM 
createdReportMM $
;MM$ %
}NN 
catchOO 
(OO 
ArgumentExceptionOO $
)OO$ %
{PP 
throwQQ 
;QQ 
}RR 
catchSS 
(SS 
	ExceptionSS 
exSS 
)SS  
{TT 
_loggerUU 
.UU 
ErrorUU 
(UU 
exUU  
,UU  !
$strUU" ^
,UU^ _
reportUU` f
.UUf g
	RequestIdUUg p
)UUp q
;UUq r
throwVV 
newVV "
ReportServiceExceptionVV 0
(VV0 1
$strVV1 J
,VVJ K
exVVL N
)VVN O
;VVO P
}WW 
}XX 	
publicZZ 
asyncZZ 
TaskZZ 
<ZZ 
ListZZ 
<ZZ 
ServiceReportZZ ,
>ZZ, -
>ZZ- .%
GetAllServiceReportsAsyncZZ/ H
(ZZH I
ServiceReportFilterZZI \
filterZZ] c
)ZZc d
{[[ 	
_logger\\ 
.\\ 
Information\\ 
(\\  
$str\\  f
)\\f g
;\\g h
try]] 
{^^ 
var__ 
reports__ 
=__ 
await__ #$
_serviceReportRepository__$ <
.__< =%
GetAllServiceReportsAsync__= V
(__V W
filter__W ]
)__] ^
;__^ _
if`` 
(`` 
!`` 
reports`` 
.`` 
Any``  
(``  !
)``! "
)``" #
{aa 
_loggerbb 
.bb 
Warningbb #
(bb# $
$strbb$ >
)bb> ?
;bb? @
throwcc 
newcc #
ReportNotFoundExceptioncc 5
(cc5 6
$strcc6 H
)ccH I
;ccI J
}dd 
_loggeree 
.ee 
Informationee #
(ee# $
$stree$ Z
,eeZ [
reportsee\ c
.eec d
Counteed i
)eei j
;eej k
returnff 
reportsff 
;ff 
}gg 
catchhh 
(hh #
ReportNotFoundExceptionhh *
)hh* +
{ii 
throwjj 
;jj 
}kk 
catchll 
(ll 
	Exceptionll 
exll 
)ll  
{mm 
_loggernn 
.nn 
Errornn 
(nn 
exnn  
,nn  !
$strnn" F
)nnF G
;nnG H
throwoo 
newoo "
ReportServiceExceptionoo 0
(oo0 1
$stroo1 H
,ooH I
exooJ L
)ooL M
;ooM N
}pp 
}qq 	
publicss 
asyncss 
Taskss 
<ss 
ServiceReportss '
>ss' (%
GetServiceReportByIdAsyncss) B
(ssB C
GuidssC G
idssH J
)ssJ K
{tt 	
_loggeruu 
.uu 
Informationuu 
(uu  
$struu  Z
,uuZ [
iduu\ ^
)uu^ _
;uu_ `
tryvv 
{ww 
varxx 
reportxx 
=xx 
awaitxx "$
_serviceReportRepositoryxx# ;
.xx; <%
GetServiceReportByIdAsyncxx< U
(xxU V
idxxV X
)xxX Y
;xxY Z
ifyy 
(yy 
reportyy 
==yy 
nullyy "
)yy" #
{zz 
_logger{{ 
.{{ 
Warning{{ #
({{# $
$str{{$ Q
,{{Q R
id{{S U
){{U V
;{{V W
throw|| 
new|| #
ReportNotFoundException|| 5
(||5 6
$"||6 8
$str||8 G
{||G H
id||H J
}||J K
$str||K U
"||U V
)||V W
;||W X
}}} 
_logger 
. 
Information #
(# $
$str$ ^
,^ _
id` b
)b c
;c d
return
ÄÄ 
report
ÄÄ 
;
ÄÄ 
}
ÅÅ 
catch
ÇÇ 
(
ÇÇ %
ReportNotFoundException
ÇÇ *
)
ÇÇ* +
{
ÉÉ 
throw
ÑÑ 
;
ÑÑ 
}
ÖÖ 
catch
ÜÜ 
(
ÜÜ 
	Exception
ÜÜ 
ex
ÜÜ 
)
ÜÜ  
{
áá 
_logger
àà 
.
àà 
Error
àà 
(
àà 
ex
àà  
,
àà  !
$str
àà" X
,
ààX Y
id
ààZ \
)
àà\ ]
;
àà] ^
throw
ââ 
new
ââ $
ReportServiceException
ââ 0
(
ââ0 1
$str
ââ1 G
,
ââG H
ex
ââI K
)
ââK L
;
ââL M
}
ää 
}
ãã 	
public
çç 
async
çç 
Task
çç 
<
çç 
ServiceReport
çç '
>
çç' (%
CloseServiceReportAsync
çç) @
(
çç@ A
Guid
ççA E
id
ççF H
,
ççH I 
ServiceReportClose
ççJ \
report
çç] c
)
ççc d
{
éé 	
_logger
èè 
.
èè 
Information
èè 
(
èè  
$str
èè  W
,
èèW X
id
èèY [
)
èè[ \
;
èè\ ]
try
êê 
{
ëë 
var
íí 
closedReport
íí  
=
íí! "
await
íí# (&
_serviceReportRepository
íí) A
.
ííA B%
CloseServiceReportAsync
ííB Y
(
ííY Z
id
ííZ \
,
íí\ ]
DateTime
ìì 
.
ìì 
Now
ìì  
.
ìì  !
ToUniversalTime
ìì! 0
(
ìì0 1
)
ìì1 2
,
ìì2 3
report
îî 
.
îî 
Price
îî  
,
îî  !
report
ïï 
.
ïï 
Description
ïï &
)
ïï& '
;
ïï' (
_logger
óó 
.
óó 
Information
óó #
(
óó# $
$str
óó$ [
,
óó[ \
id
óó] _
)
óó_ `
;
óó` a
var
ôô 
closedRequest
ôô !
=
ôô" #
await
ôô$ )'
_serviceRequestRepository
ôô* C
.
ôôC D'
UpdateServiceRequestAsync
ôôD ]
(
ôô] ^
closedReport
ôô^ j
.
ôôj k
	RequestId
ôôk t
,
ôôt u
new
öö "
ServiceRequestUpdate
öö ,
(
öö, -
status
öö- 3
:
öö3 4
RequestStatusType
öö5 F
.
ööF G
Closed
ööG M
)
ööM N
)
ööN O
;
ööO P
_logger
úú 
.
úú 
Information
úú #
(
úú# $
$str
úú$ N
,
úúN O
closedReport
úúP \
.
úú\ ]
	RequestId
úú] f
)
úúf g
;
úúg h
var
ûû  
additionalDownTime
ûû &
=
ûû' (
(
ûû) *
int
ûû* -
)
ûû- .
(
ûû/ 0
closedReport
ûû0 <
.
ûû< =
	CloseDate
ûû= F
.
ûûF G
Value
ûûG L
-
ûûM N
closedRequest
ûûO \
.
ûû\ ]
RequestDate
ûû] h
)
ûûh i
.
ûûi j

TotalHours
ûûj t
;
ûût u
var
üü 
assemblyLine
üü  
=
üü! "
await
üü# (%
_assemblyLineRepository
üü) @
.
üü@ A&
GetAssemblyLineByIdAsync
üüA Y
(
üüY Z
closedReport
üüZ f
.
üüf g
LineId
üüg m
)
üüm n
;
üün o
var
†† 
newDownTime
†† 
=
††  !
assemblyLine
††" .
.
††. /
DownTime
††/ 7
+
††8 9 
additionalDownTime
††: L
;
††L M
if
¢¢ 
(
¢¢ 
closedRequest
¢¢ !
.
¢¢! "
Type
¢¢" &
==
¢¢' )
RequestType
¢¢* 5
.
¢¢5 6

Inspection
¢¢6 @
)
¢¢@ A
{
££ 
var
§§ #
newLastInspectionDate
§§ -
=
§§. /
closedReport
§§0 <
.
§§< =
	CloseDate
§§= F
.
§§F G
Value
§§G L
.
§§L M
Date
§§M Q
;
§§Q R
var
•• #
newNextInspectionDate
•• -
=
••. /#
newLastInspectionDate
••0 E
.
••E F
	AddMonths
••F O
(
••O P
$num
••P R
/
••S T
assemblyLine
••U a
.
••a b 
InspectionsPerYear
••b t
)
••t u
;
••u v
await
ßß %
_assemblyLineRepository
ßß 1
.
ßß1 2%
UpdateAssemblyLineAsync
ßß2 I
(
ßßI J
closedReport
®® $
.
®®$ %
LineId
®®% +
,
®®+ ,
new
©©  
AssemblyLineUpdate
©© .
(
©©. /
status
™™ "
:
™™" #
LineStatusType
™™$ 2
.
™™2 3
Working
™™3 :
,
™™: ;
downTime
´´ $
:
´´$ %
newDownTime
´´& 1
,
´´1 2
lastInspection
¨¨ *
:
¨¨* +
DateOnly
¨¨, 4
.
¨¨4 5
FromDateTime
¨¨5 A
(
¨¨A B#
newLastInspectionDate
¨¨B W
)
¨¨W X
,
¨¨X Y
nextInspection
≠≠ *
:
≠≠* +
DateOnly
≠≠, 4
.
≠≠4 5
FromDateTime
≠≠5 A
(
≠≠A B#
newNextInspectionDate
≠≠B W
)
≠≠W X
)
≠≠X Y
)
≠≠Y Z
;
≠≠Z [
_logger
ØØ 
.
ØØ 
Information
ØØ '
(
ØØ' (
$str
ØØ( e
,
ØØe f
closedReport
∞∞ $
.
∞∞$ %
LineId
∞∞% +
)
∞∞+ ,
;
∞∞, -
}
±± 
else
≤≤ 
{
≥≥ 
await
¥¥ %
_assemblyLineRepository
¥¥ 1
.
¥¥1 2%
UpdateAssemblyLineAsync
¥¥2 I
(
¥¥I J
closedReport
¥¥J V
.
¥¥V W
LineId
¥¥W ]
,
¥¥] ^
new
µµ  
AssemblyLineUpdate
µµ .
(
µµ. /
status
µµ/ 5
:
µµ5 6
LineStatusType
µµ7 E
.
µµE F
Working
µµF M
,
µµM N
downTime
∂∂ $
:
∂∂$ %
newDownTime
∂∂& 1
)
∂∂1 2
)
∂∂2 3
;
∂∂3 4
_logger
∏∏ 
.
∏∏ 
Information
∏∏ '
(
∏∏' (
$str
∏∏( ^
,
∏∏^ _
closedReport
∏∏` l
.
∏∏l m
LineId
∏∏m s
)
∏∏s t
;
∏∏t u
}
ππ 
return
ªª 
closedReport
ªª #
;
ªª# $
}
ºº 
catch
ΩΩ 
(
ΩΩ %
ReportNotFoundException
ΩΩ *
)
ΩΩ* +
{
ææ 
throw
øø 
;
øø 
}
¿¿ 
catch
¡¡ 
(
¡¡ 
	Exception
¡¡ 
ex
¡¡ 
)
¡¡  
{
¬¬ 
_logger
√√ 
.
√√ 
Error
√√ 
(
√√ 
ex
√√  
,
√√  !
$str
√√" U
,
√√U V
id
√√W Y
)
√√Y Z
;
√√Z [
throw
ƒƒ 
new
ƒƒ $
ReportServiceException
ƒƒ 0
(
ƒƒ0 1
$str
ƒƒ1 G
,
ƒƒG H
ex
ƒƒI K
)
ƒƒK L
;
ƒƒL M
}
≈≈ 
}
∆∆ 	
}
«« 
}»» Å
x/Users/m4ks0n/study/IU7/sem7/testing/bmstu-iu7-sem7-web/src/ProdMonitor/ProdMonitor.Application/Services/EmailService.cs
	namespace 	
ProdMonitor
 
. 
Application !
.! "
Services" *
;* +
public 
class 
EmailService 
: 
IEmailService (
{		 
private

 
readonly

 
IConfiguration

 #
_configuration

$ 2
;

2 3
public 

EmailService 
( 
IConfiguration &
configuration' 4
)4 5
{ 
_configuration 
= 
configuration &
;& '
} 
public 

async 
Task 
SendEmailAsync $
($ %
string% +
to, .
,. /
string0 6
subject7 >
,> ?
string@ F
bodyG K
)K L
{ 
var 

smtpServer 
= 
_configuration '
[' (
$str( B
]B C
;C D
var 
port 
= 
int 
. 
Parse 
( 
_configuration +
[+ ,
$str, @
]@ A
)A B
;B C
var 
senderEmail 
= 
_configuration (
[( )
$str) D
]D E
;E F
var 
senderPassword 
= 
_configuration +
[+ ,
$str, J
]J K
;K L
using 
var 
client 
= 
new 

SmtpClient )
() *

smtpServer* 4
,4 5
port6 :
): ;
{ 	
Credentials 
= 
new 
NetworkCredential /
(/ 0
senderEmail0 ;
,; <
senderPassword= K
)K L
,L M
	EnableSsl 
= 
true 
} 	
;	 

var 
message 
= 
new 
MailMessage %
{ 	
From   
=   
new   
MailAddress   "
(  " #
senderEmail  # .
,  . /
_configuration  0 >
[  > ?
$str  ? Y
]  Y Z
)  Z [
,  [ \
Subject!! 
=!! 
subject!! 
,!! 
Body"" 
="" 
body"" 
,"" 

IsBodyHtml## 
=## 
true## 
}$$ 	
;$$	 

message&& 
.&& 
To&& 
.&& 
Add&& 
(&& 
to&& 
)&& 
;&& 
await(( 
client(( 
.(( 
SendMailAsync(( "
(((" #
message((# *
)((* +
;((+ ,
})) 
}** ﬁ;
y/Users/m4ks0n/study/IU7/sem7/testing/bmstu-iu7-sem7-web/src/ProdMonitor/ProdMonitor.Application/Services/DetailService.cs
	namespace 	
ProdMonitor
 
. 
Application !
.! "
Services" *
{ 
public		 

class		 
DetailService		 
:		  
IDetailService		! /
{

 
private 
readonly 
IDetailRepository *
_detailRepository+ <
;< =
private 
readonly 
ILogger  
_logger! (
;( )
private 
IDetailService (
_detailServiceImplementation ;
;; <
public 
DetailService 
( 
IDetailRepository .
detailRepository/ ?
,? @
ILogger 
logger 
) 
{ 	
_detailRepository 
= 
detailRepository  0
;0 1
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Detail  
>  !
CreateDetailAsync" 3
(3 4
DetailCreate4 @
detailA G
)G H
{ 	
_logger 
. 
Information 
(  
$str  Z
,Z [
detail\ b
.b c
Namec g
)g h
;h i
try 
{ 
var 
createdDetail !
=" #
await$ )
_detailRepository* ;
.; <
CreateDetailAsync< M
(M N
detailN T
)T U
;U V
_logger 
. 
Information #
(# $
$str$ Z
,Z [
createdDetail\ i
.i j
Idj l
)l m
;m n
return 
createdDetail $
;$ %
} 
catch 
( 
	Exception 
ex 
)  
{   
_logger!! 
.!! 
Error!! 
(!! 
ex!!  
,!!  !
$str!!" R
,!!R S
detail!!T Z
.!!Z [
Name!![ _
)!!_ `
;!!` a
throw"" 
new"" "
DetailServiceException"" 0
(""0 1
$str""1 J
,""J K
ex""L N
)""N O
;""O P
}## 
}$$ 	
public&& 
async&& 
Task&& 
<&& 
List&& 
<&& 
Detail&& %
>&&% &
>&&& '
GetAllDetailsAsync&&( :
(&&: ;
DetailFilter&&; G
filter&&H N
)&&N O
{'' 	
_logger(( 
.(( 
Information(( 
(((  
$str((  ^
)((^ _
;((_ `
try** 
{++ 
var,, 
details,, 
=,, 
await,, #
_detailRepository,,$ 5
.,,5 6
GetAllDetailsAsync,,6 H
(,,H I
filter,,I O
),,O P
;,,P Q
if-- 
(-- 
!-- 
details-- 
.-- 
Any--  
(--  !
)--! "
)--" #
{.. 
_logger// 
.// 
Warning// #
(//# $
$str//$ 6
)//6 7
;//7 8
throw00 
new00 #
DetailNotFoundException00 5
(005 6
$str006 H
)00H I
;00I J
}11 
_logger33 
.33 
Information33 #
(33# $
$str33$ R
,33R S
details33T [
.33[ \
Count33\ a
)33a b
;33b c
return44 
details44 
;44 
}55 
catch66 
(66 #
DetailNotFoundException66 *
)66* +
{77 
throw88 
;88 
}99 
catch:: 
(:: 
	Exception:: 
ex:: 
)::  
{;; 
_logger<< 
.<< 
Error<< 
(<< 
ex<<  
,<<  !
$str<<" >
)<<> ?
;<<? @
throw== 
new== "
DetailServiceException== 0
(==0 1
$str==1 H
,==H I
ex==J L
)==L M
;==M N
}>> 
}?? 	
publicAA 
asyncAA 
TaskAA 
<AA 
DetailAA  
>AA  !
GetDetailByIdAsyncAA" 4
(AA4 5
GuidAA5 9
idAA: <
)AA< =
{BB 	
_loggerCC 
.CC 
InformationCC 
(CC  
$strCC  R
,CCR S
idCCT V
)CCV W
;CCW X
tryDD 
{EE 
varFF 
detailFF 
=FF 
awaitFF "
_detailRepositoryFF# 4
.FF4 5
GetDetailByIdAsyncFF5 G
(FFG H
idFFH J
)FFJ K
;FFK L
ifGG 
(GG 
detailGG 
==GG 
nullGG "
)GG" #
{HH 
_loggerII 
.II 
WarningII #
(II# $
$strII$ I
,III J
idIIK M
)IIM N
;IIN O
throwJJ 
newJJ #
DetailNotFoundExceptionJJ 5
(JJ5 6
$"JJ6 8
$strJJ8 G
{JJG H
idJJH J
}JJJ K
$strJJK U
"JJU V
)JJV W
;JJW X
}KK 
_loggerMM 
.MM 
InformationMM #
(MM# $
$strMM$ V
,MMV W
idMMX Z
)MMZ [
;MM[ \
returnNN 
detailNN 
;NN 
}OO 
catchPP 
(PP #
DetailNotFoundExceptionPP *
)PP* +
{QQ 
throwRR 
;RR 
}SS 
catchTT 
(TT 
	ExceptionTT 
exTT 
)TT  
{UU 
_loggerVV 
.VV 
ErrorVV 
(VV 
exVV  
,VV  !
$strVV" P
,VVP Q
idVVR T
)VVT U
;VVU V
throwWW 
newWW "
DetailServiceExceptionWW 0
(WW0 1
$strWW1 G
,WWG H
exWWI K
)WWK L
;WWL M
}XX 
}YY 	
public[[ 
Task[[ 
DeleteDetailAsync[[ %
([[% &
Guid[[& *
id[[+ -
)[[- .
{\\ 	
_logger]] 
.]] 
Information]] 
(]]  
$str]]  P
,]]P Q
id]]R T
)]]T U
;]]U V
try^^ 
{__ 
return`` 
_detailRepository`` (
.``( )
DeleteDetailAsync``) :
(``: ;
id``; =
)``= >
;``> ?
}aa 
catchbb 
(bb #
DetailNotFoundExceptionbb *
)bb* +
{cc 
throwdd 
;dd 
}ee 
catchff 
(ff 
	Exceptionff 
exff 
)ff  
{gg 
_loggerhh 
.hh 
Errorhh 
(hh 
exhh  
,hh  !
$strhh" N
,hhN O
idhhP R
)hhR S
;hhS T
throwii 
newii "
DetailServiceExceptionii 0
(ii0 1
$strii1 J
,iiJ K
exiiL N
)iiN O
;iiO P
}jj 
}kk 	
}ll 
}mm ˝L
~/Users/m4ks0n/study/IU7/sem7/testing/bmstu-iu7-sem7-web/src/ProdMonitor/ProdMonitor.Application/Services/DetailOrderService.cs
	namespace 	
ProdMonitor
 
. 
Application !
.! "
Services" *
{		 
public

 

class

 
DetailOrderService

 #
:

$ %
IDetailOrderService

& 9
{ 
private 
readonly "
IDetailOrderRepository /
_orderRepository0 @
;@ A
private 
readonly "
IOrderDetailRepository /"
_orderDetailRepository0 F
;F G
private 
readonly 
IDetailRepository *
_detailRepository+ <
;< =
private 
readonly 
ILogger  
_logger! (
;( )
public 
DetailOrderService !
(! ""
IDetailOrderRepository" 8
orderRepository9 H
,H I"
IOrderDetailRepository "!
orderDetailRepository# 8
,8 9
IDetailRepository 
detailRepository .
,. /
ILogger 
logger 
) 
{ 	
_orderRepository 
= 
orderRepository .
;. /"
_orderDetailRepository "
=# $!
orderDetailRepository% :
;: ;
_detailRepository 
= 
detailRepository  0
;0 1
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
DetailOrder %
>% &"
CreateDetailOrderAsync' =
(= >
DetailOrderCreate> O
orderP U
)U V
{ 	
_logger 
. 
Information 
(  
$str  [
,[ \
order] b
.b c
UserIdc i
)i j
;j k
try 
{   
if!! 
(!! 
order!! 
.!! 
OrderDetails!! &
==!!' )
null!!* .
||!!/ 1
!!!2 3
order!!3 8
.!!8 9
OrderDetails!!9 E
.!!E F
Any!!F I
(!!I J
)!!J K
)!!K L
{"" 
_logger## 
.## 
Warning## #
(### $
$str##$ o
,##o p
order##q v
.##v w
UserId##w }
)##} ~
;##~ 
throw$$ 
new$$ 
ArgumentException$$ /
($$/ 0
$str$$0 Y
)$$Y Z
;$$Z [
}%% 
var'' 

totalPrice'' 
=''  
$num''! #
;''# $
foreach(( 
((( 
OrderDetailData(( (
orderDetail(() 4
in((5 7
order((8 =
.((= >
OrderDetails((> J
)((J K
{)) 
var** 
detail** 
=**  
await**! &
_detailRepository**' 8
.**8 9
GetDetailByIdAsync**9 K
(**K L
orderDetail**L W
.**W X
DetailId**X `
)**` a
;**a b
if++ 
(++ 
detail++ 
==++ !
null++" &
)++& '
{,, 
_logger-- 
.--  
Warning--  '
(--' (
$str--( k
,--k l
orderDetail--m x
.--x y
DetailId	--y Å
)
--Å Ç
;
--Ç É
throw.. 
new.. !#
DetailNotFoundException.." 9
(..9 :
$"..: <
$str..< K
{..K L
orderDetail..L W
...W X
DetailId..X `
}..` a
$str..a k
"..k l
)..l m
;..m n
}// 

totalPrice00 
+=00 !
detail00" (
.00( )
Price00) .
*00/ 0
orderDetail001 <
.00< =
DetailsAmount00= J
;00J K
}11 
var33 
newOrder33 
=33 
await33 $
_orderRepository33% 5
.335 6"
CreateDetailOrderAsync336 L
(33L M
userId33M S
:33S T
order33U Z
.33Z [
UserId33[ a
,33a b
status44 
:44 !
DetailOrderStatusType44 1
.441 2
InWork442 8
,448 9

totalPrice55 
:55 

totalPrice55  *
,55* +
	orderDate66 
:66 
DateTime66 '
.66' (
Now66( +
.66+ ,
ToUniversalTime66, ;
(66; <
)66< =
,66= >
orderDetails77  
:77  !
order77" '
.77' (
OrderDetails77( 4
)774 5
;775 6
_logger99 
.99 
Information99 #
(99# $
$str99$ q
,99q r
newOrder99s {
.99{ |
Id99| ~
,99~ 
order
99Ä Ö
.
99Ö Ü
UserId
99Ü å
)
99å ç
;
99ç é
return:: 
newOrder:: 
;::  
};; 
catch<< 
(<< #
DetailNotFoundException<< *
)<<* +
{== 
throw>> 
;>> 
}?? 
catch@@ 
(@@ 
ArgumentException@@ $
)@@$ %
{AA 
throwBB 
;BB 
}CC 
catchDD 
(DD 
	ExceptionDD 
exDD 
)DD  
{EE 
_loggerFF 
.FF 
ErrorFF 
(FF 
exFF  
,FF  !
$strFF" R
,FFR S
orderFFT Y
.FFY Z
UserIdFFZ `
)FF` a
;FFa b
throwGG 
newGG  
DetailOrderExceptionGG .
(GG. /
$strGG/ G
,GGG H
exGGI K
)GGK L
;GGL M
}HH 
}II 	
publicKK 
asyncKK 
TaskKK 
<KK 
ListKK 
<KK 
DetailOrderKK *
>KK* +
>KK+ ,#
GetAllDetailOrdersAsyncKK- D
(KKD E
DetailOrderFilterKKE V
filterKKW ]
)KK] ^
{LL 	
_loggerMM 
.MM 
InformationMM 
(MM  
$strMM  d
)MMd e
;MMe f
tryNN 
{OO 
varPP 
ordersPP 
=PP 
awaitPP "
_orderRepositoryPP# 3
.PP3 4#
GetAllDetailOrdersAsyncPP4 K
(PPK L
filterPPL R
)PPR S
;PPS T
ifQQ 
(QQ 
!QQ 
ordersQQ 
.QQ 
AnyQQ 
(QQ  
)QQ  !
)QQ! "
{RR 
_loggerSS 
.SS 
WarningSS #
(SS# $
$strSS$ <
)SS< =
;SS= >
throwTT 
newTT (
DetailOrderNotFoundExceptionTT :
(TT: ;
$strTT; S
)TTS T
;TTT U
}UU 
_loggerVV 
.VV 
InformationVV #
(VV# $
$strVV$ W
,VVW X
ordersVVY _
.VV_ `
CountVV` e
)VVe f
;VVf g
returnWW 
ordersWW 
;WW 
}XX 
catchYY 
(YY 
	ExceptionYY 
exYY 
)YY  
{ZZ 
_logger[[ 
.[[ 
Error[[ 
([[ 
ex[[  
,[[  !
$str[[" D
)[[D E
;[[E F
throw\\ 
new\\  
DetailOrderException\\ .
(\\. /
$str\\/ L
,\\L M
ex\\N P
)\\P Q
;\\Q R
}]] 
}^^ 	
public`` 
async`` 
Task`` 
<`` 
DetailOrder`` %
>``% &
GetDetailOrderById``' 9
(``9 :
Guid``: >
id``? A
)``A B
{aa 	
_loggerbb 
.bb 
Informationbb 
(bb  
$strbb  W
,bbW X
idbbY [
)bb[ \
;bb\ ]
trycc 
{dd 
varee 
orderee 
=ee 
awaitee !
_orderRepositoryee" 2
.ee2 3#
GetDetailOrderByIdAsyncee3 J
(eeJ K
ideeK M
)eeM N
;eeN O
ifff 
(ff 
orderff 
==ff 
nullff !
)ff! "
{gg 
_loggerhh 
.hh 
Warninghh #
(hh# $
$strhh$ N
,hhN O
idhhP R
)hhR S
;hhS T
throwii 
newii (
DetailOrderNotFoundExceptionii :
(ii: ;
$"ii; =
$strii= R
{iiR S
idiiS U
}iiU V
$striiV `
"ii` a
)iia b
;iib c
}jj 
_loggerll 
.ll 
Informationll #
(ll# $
$strll$ [
,ll[ \
idll] _
)ll_ `
;ll` a
returnmm 
ordermm 
;mm 
}nn 
catchoo 
(oo (
DetailOrderNotFoundExceptionoo /
)oo/ 0
{pp 
throwqq 
;qq 
}rr 
catchss 
(ss 
	Exceptionss 
exss 
)ss  
{tt 
_loggeruu 
.uu 
Erroruu 
(uu 
exuu  
,uu  !
$struu" U
,uuU V
iduuW Y
)uuY Z
;uuZ [
throwvv 
newvv  
DetailOrderExceptionvv .
(vv. /
$strvv/ K
,vvK L
exvvM O
)vvO P
;vvP Q
}ww 
}xx 	
}yy 
}zz ∂
ù/Users/m4ks0n/study/IU7/sem7/testing/bmstu-iu7-sem7-web/src/ProdMonitor/ProdMonitor.Application/Services/Configurations/AuthenticationServiceConfiguration.cs
	namespace 	
ProdMonitor
 
. 
Application !
.! "
Services" *
.* +
Configurations+ 9
{ 
public 

class .
"AuthenticationServiceConfiguration 3
{ 
public 
static 
readonly 
string %$
ConfigurationSectionName& >
=? @
$strA P
;P Q
public 
required 
int 
MinPasswordLength -
{. /
get0 3
;3 4
init5 9
;9 :
}; <
} 
} •‘
Å/Users/m4ks0n/study/IU7/sem7/testing/bmstu-iu7-sem7-web/src/ProdMonitor/ProdMonitor.Application/Services/AuthenticationService.cs
	namespace

 	
ProdMonitor


 
.

 
Application

 !
.

! "
Services

" *
{ 
public 

class !
AuthenticationService &
:' ("
IAuthenticationService) ?
{ 
private 
readonly 
IUserRepository (
_userRepository) 8
;8 9
private 
readonly 
ILogger  
_logger! (
;( )
private 
readonly 
IEmailService &
_emailService' 4
;4 5
private 
readonly .
"AuthenticationServiceConfiguration ;/
#_authenticationServiceConfiguration< _
;_ `
public !
AuthenticationService $
($ %
IUserRepository% 4
userRepository5 C
,C D
ILogger 
logger 
, 
IEmailService 
emailService &
,& '
IOptions 
< .
"AuthenticationServiceConfiguration 7
>7 8.
"authenticationServiceConfiguration9 [
)[ \
{ 	
_userRepository 
= 
userRepository ,
;, -
_logger 
= 
logger 
; 
_emailService 
= 
emailService (
;( )/
#_authenticationServiceConfiguration /
=0 1.
"authenticationServiceConfiguration2 T
.T U
ValueU Z
;Z [
} 	
public 
async 
Task 
< 
User 
> 

LoginAsync  *
(* +

LoginModel+ 5
	authModel6 ?
)? @
{ 	
try   
{!! 
_logger"" 
."" 
Information"" #
(""# $
$str""$ J
,""J K
	authModel""L U
.""U V
Email""V [
)""[ \
;""\ ]
var## 
user## 
=## 
await##  
_userRepository##! 0
.##0 1
GetUserByEmailAsync##1 D
(##D E
	authModel##E N
.##N O
Email##O T
)##T U
;##U V
if$$ 
($$ 
user$$ 
==$$ 
null$$  
)$$  !
{%% 
_logger&& 
.&& 
Warning&& #
(&&# $
$str&&$ U
,&&U V
	authModel&&W `
.&&` a
Email&&a f
)&&f g
;&&g h
throw'' 
new'' !
UserNotFoundException'' 3
(''3 4
$"''4 6
$str''6 F
{''F G
	authModel''G P
.''P Q
Email''Q V
}''V W
$str''W a
"''a b
)''b c
;''c d
}(( 
if** 
(** 
!** 
VerifyPasswordHash** '
(**' (
	authModel**( 1
.**1 2
Password**2 :
,**: ;
user**< @
.**@ A
PasswordHash**A M
,**M N
user**O S
.**S T
PasswordSalt**T `
)**` a
)**a b
{++ 
_logger,, 
.,, 
Warning,, #
(,,# $
$str,,$ X
,,,X Y
	authModel,,Z c
.,,c d
Email,,d i
),,i j
;,,j k
throw-- 
new-- "
WrongPasswordException-- 4
(--4 5
$str--5 E
)--E F
;--F G
}.. 
_logger00 
.00 
Information00 #
(00# $
$str00$ I
,00I J
	authModel00K T
.00T U
Email00U Z
)00Z [
;00[ \
return11 
user11 
;11 
}22 
catch33 
(33 !
UserNotFoundException33 (
)33( )
{44 
throw55 
;55 
}66 
catch77 
(77 "
WrongPasswordException77 )
)77) *
{88 
throw99 
;99 
}:: 
catch;; 
(;; 
	Exception;; 
ex;; 
);;  
{<< 
_logger== 
.== 
Error== 
(== 
ex==  
,==  !
$str==" B
,==B C
	authModel==D M
.==M N
Email==N S
)==S T
;==T U
throw>> 
new>> 
LoginException>> (
(>>( )
$str>>) :
,>>: ;
ex>>< >
)>>> ?
;>>? @
}?? 
}@@ 	
publicBB 
asyncBB 
TaskBB 
<BB 
UserBB 
>BB 
RegisterAsyncBB  -
(BB- .
RegisterModelBB. ;
	authModelBB< E
)BBE F
{CC 	
tryDD 
{EE 
_loggerFF 
.FF 
InformationFF #
(FF# $
$strFF$ V
,FFV W
	authModelFFX a
.FFa b
EmailFFb g
)FFg h
;FFh i
ifGG 
(GG 
	authModelGG 
.GG 
PasswordGG &
.GG& '
LengthGG' -
<GG. //
#_authenticationServiceConfigurationGG0 S
.GGS T
MinPasswordLengthGGT e
)GGe f
{HH 
_loggerII 
.II 
ErrorII !
(II! "
$strJJ n
,JJn o/
#_authenticationServiceConfigurationKK ;
.KK; <
MinPasswordLengthKK< M
)KKM N
;KKN O
throwLL 
newLL 
ArgumentExceptionLL /
(LL/ 0
$"MM 
$strMM F
{MMF G/
#_authenticationServiceConfigurationMMG j
.MMj k
MinPasswordLengthMMk |
}MM| }
"MM} ~
)MM~ 
;	MM Ä
}NN 
varPP 
existingUserPP  
=PP! "
awaitPP# (
_userRepositoryPP) 8
.PP8 9
GetUserByEmailAsyncPP9 L
(PPL M
	authModelPPM V
.PPV W
EmailPPW \
)PP\ ]
;PP] ^
ifQQ 
(QQ 
existingUserQQ  
!=QQ! #
nullQQ$ (
)QQ( )
{RR 
_loggerSS 
.SS 
WarningSS #
(SS# $
$strSS$ a
,SSa b
	authModelSSc l
.SSl m
EmailSSm r
)SSr s
;SSs t
throwTT 
newTT %
UserAlreadyExistExceptionTT 7
(TT7 8
$"TT8 :
$strTT: J
{TTJ K
	authModelTTK T
.TTT U
EmailTTU Z
}TTZ [
$strTT[ i
"TTi j
)TTj k
;TTk l
}UU 
byteWW 
[WW 
]WW 
passwordHashWW #
,WW# $
passwordSaltWW% 1
;WW1 2
CreatePasswordHashXX "
(XX" #
	authModelXX# ,
.XX, -
PasswordXX- 5
,XX5 6
outXX7 :
passwordHashXX; G
,XXG H
outXXI L
passwordSaltXXM Y
)XXY Z
;XXZ [
varZZ 
newUserZZ 
=ZZ 
newZZ !

UserCreateZZ" ,
(ZZ, -
name[[ 
:[[ 
	authModel[[ #
.[[# $
Name[[$ (
,[[( )
surname\\ 
:\\ 
	authModel\\ &
.\\& '
Surname\\' .
,\\. /

patronymic]] 
:]] 
	authModel]]  )
.]]) *

Patronymic]]* 4
,]]4 5

department^^ 
:^^ 
	authModel^^  )
.^^) *

Department^^* 4
,^^4 5
email__ 
:__ 
	authModel__ $
.__$ %
Email__% *
,__* +
passwordHash``  
:``  !
passwordHash``" .
,``. /
passwordSaltaa  
:aa  !
passwordSaltaa" .
,aa. /
birthDaybb 
:bb 
	authModelbb '
.bb' (
BirthDaybb( 0
,bb0 1
sexcc 
:cc 
	authModelcc "
.cc" #
Sexcc# &
,cc& '
roledd 
:dd 
RoleTypedd "
.dd" #
Verificationdd# /
)dd/ 0
;dd0 1
varff 
createdUserff 
=ff  !
awaitff" '
_userRepositoryff( 7
.ff7 8
CreateUserAsyncff8 G
(ffG H
newUserffH O
)ffO P
;ffP Q
_loggergg 
.gg 
Informationgg #
(gg# $
$strgg$ U
,ggU V
	authModelggW `
.gg` a
Emailgga f
)ggf g
;ggg h
returnhh 
createdUserhh "
;hh" #
}ii 
catchjj 
(jj 
ArgumentExceptionjj $
)jj$ %
{kk 
throwll 
;ll 
}mm 
catchnn 
(nn %
UserAlreadyExistExceptionnn ,
)nn, -
{oo 
throwpp 
;pp 
}qq 
catchrr 
(rr 
	Exceptionrr 
exrr 
)rr  
{ss 
_loggertt 
.tt 
Errortt 
(tt 
extt  
,tt  !
$strtt" I
,ttI J
	authModelttK T
.ttT U
EmailttU Z
)ttZ [
;tt[ \
throwuu 
newuu 
RegisterExceptionuu +
(uu+ ,
$struu, @
,uu@ A
exuuB D
)uuD E
;uuE F
}vv 
}ww 	
publicyy 
asyncyy 
Taskyy 
<yy 
Useryy 
>yy 
ChangePasswordAsyncyy  3
(yy3 4
Guidyy4 8
userIdyy9 ?
,yy? @
stringyyA G
newPasswordyyH S
,yyS T
stringyyU [
oldPasswordyy\ g
)yyg h
{zz 	
try{{ 
{|| 
_logger}} 
.}} 
Information}} #
(}}# $
$str}}$ Z
,}}Z [
userId}}\ b
)}}b c
;}}c d
if~~ 
(~~ 
newPassword~~ 
.~~  
Length~~  &
<~~' (/
#_authenticationServiceConfiguration~~) L
.~~L M
MinPasswordLength~~M ^
)~~^ _
{ 
_logger
ÄÄ 
.
ÄÄ 
Error
ÄÄ !
(
ÄÄ! "
$str
ÅÅ q
,
ÅÅq r1
#_authenticationServiceConfiguration
ÇÇ ;
.
ÇÇ; <
MinPasswordLength
ÇÇ< M
)
ÇÇM N
;
ÇÇN O
throw
ÉÉ 
new
ÉÉ 
ArgumentException
ÉÉ /
(
ÉÉ/ 0
$"
ÑÑ 
$str
ÑÑ F
{
ÑÑF G1
#_authenticationServiceConfiguration
ÑÑG j
.
ÑÑj k
MinPasswordLength
ÑÑk |
}
ÑÑ| }
"
ÑÑ} ~
)
ÑÑ~ 
;ÑÑ Ä
}
ÖÖ 
var
áá 
user
áá 
=
áá 
await
áá  
_userRepository
áá! 0
.
áá0 1
GetUserByIdAsync
áá1 A
(
ááA B
userId
ááB H
)
ááH I
;
ááI J
if
àà 
(
àà 
user
àà 
==
àà 
null
àà  
)
àà  !
{
ââ 
_logger
ää 
.
ää 
Warning
ää #
(
ää# $
$str
ää$ ]
,
ää] ^
userId
ää_ e
)
ääe f
;
ääf g
throw
ãã 
new
ãã #
UserNotFoundException
ãã 3
(
ãã3 4
$"
ãã4 6
$str
ãã6 C
{
ããC D
userId
ããD J
}
ããJ K
$str
ããK U
"
ããU V
)
ããV W
;
ããW X
}
åå 
if
éé 
(
éé 
!
éé  
VerifyPasswordHash
éé '
(
éé' (
oldPassword
éé( 3
,
éé3 4
user
éé5 9
.
éé9 :
PasswordHash
éé: F
,
ééF G
user
ééH L
.
ééL M
PasswordSalt
ééM Y
)
ééY Z
)
ééZ [
{
èè 
_logger
êê 
.
êê 
Warning
êê #
(
êê# $
$str
êê$ n
,
êên o
userId
êêp v
)
êêv w
;
êêw x
throw
ëë 
new
ëë $
WrongPasswordException
ëë 4
(
ëë4 5
$str
ëë5 E
)
ëëE F
;
ëëF G
}
íí 
byte
îî 
[
îî 
]
îî 
passwordHash
îî #
,
îî# $
passwordSalt
îî% 1
;
îî1 2 
CreatePasswordHash
ïï "
(
ïï" #
newPassword
ïï# .
,
ïï. /
out
ïï0 3
passwordHash
ïï4 @
,
ïï@ A
out
ïïB E
passwordSalt
ïïF R
)
ïïR S
;
ïïS T
var
óó 
updatedUser
óó 
=
óó  !
new
óó" %

UserCreate
óó& 0
(
óó0 1
name
óó1 5
:
óó5 6
user
óó7 ;
.
óó; <
Name
óó< @
,
óó@ A
surname
òò 
:
òò 
user
òò !
.
òò! "
Surname
òò" )
,
òò) *

patronymic
ôô 
:
ôô 
user
ôô  $
.
ôô$ %

Patronymic
ôô% /
,
ôô/ 0

department
öö 
:
öö 
user
öö  $
.
öö$ %

Department
öö% /
,
öö/ 0
email
õõ 
:
õõ 
user
õõ 
.
õõ  
Email
õõ  %
,
õõ% &
passwordHash
úú  
:
úú  !
passwordHash
úú" .
,
úú. /
passwordSalt
ùù  
:
ùù  !
passwordSalt
ùù" .
,
ùù. /
birthDay
ûû 
:
ûû 
user
ûû "
.
ûû" #
BirthDay
ûû# +
,
ûû+ ,
sex
üü 
:
üü 
user
üü 
.
üü 
Sex
üü !
,
üü! "
role
†† 
:
†† 
user
†† 
.
†† 
Role
†† #
)
††# $
;
††$ %
var
¢¢ 
changedUser
¢¢ 
=
¢¢  !
await
¢¢" '
_userRepository
¢¢( 7
.
¢¢7 8
UpdateUserAsync
¢¢8 G
(
¢¢G H
userId
¢¢H N
,
¢¢N O
updatedUser
¢¢P [
)
¢¢[ \
;
¢¢\ ]
_logger
§§ 
.
§§ 
Information
§§ #
(
§§# $
$str
§§$ ]
,
§§] ^
userId
§§_ e
)
§§e f
;
§§f g
return
•• 
changedUser
•• "
;
••" #
}
¶¶ 
catch
ßß 
(
ßß 
ArgumentException
ßß $
)
ßß$ %
{
®® 
throw
©© 
;
©© 
}
™™ 
catch
´´ 
(
´´ #
UserNotFoundException
´´ (
)
´´( )
{
¨¨ 
throw
≠≠ 
;
≠≠ 
}
ÆÆ 
catch
ØØ 
(
ØØ 
	Exception
ØØ 
ex
ØØ 
)
ØØ  
{
∞∞ 
_logger
±± 
.
±± 
Error
±± 
(
±± 
ex
±±  
,
±±  !
$str
±±" T
,
±±T U
userId
±±V \
)
±±\ ]
;
±±] ^
throw
≤≤ 
new
≤≤ "
UserServiceException
≤≤ .
(
≤≤. /
$str
≤≤/ J
,
≤≤J K
ex
≤≤L N
)
≤≤N O
;
≤≤O P
}
≥≥ 
}
¥¥ 	
public
∂∂ 
void
∂∂  
CreatePasswordHash
∂∂ &
(
∂∂& '
string
∂∂' -
password
∂∂. 6
,
∂∂6 7
out
∂∂8 ;
byte
∂∂< @
[
∂∂@ A
]
∂∂A B
passwordHash
∂∂C O
,
∂∂O P
out
∂∂Q T
byte
∂∂U Y
[
∂∂Y Z
]
∂∂Z [
passwordSalt
∂∂\ h
)
∂∂h i
{
∑∑ 	
using
∏∏ 
(
∏∏ 
var
∏∏ 
hmac
∏∏ 
=
∏∏ 
new
∏∏ !
System
∏∏" (
.
∏∏( )
Security
∏∏) 1
.
∏∏1 2
Cryptography
∏∏2 >
.
∏∏> ?

HMACSHA512
∏∏? I
(
∏∏I J
)
∏∏J K
)
∏∏K L
{
ππ 
passwordSalt
∫∫ 
=
∫∫ 
hmac
∫∫ #
.
∫∫# $
Key
∫∫$ '
;
∫∫' (
passwordHash
ªª 
=
ªª 
hmac
ªª #
.
ªª# $
ComputeHash
ªª$ /
(
ªª/ 0
System
ªª0 6
.
ªª6 7
Text
ªª7 ;
.
ªª; <
Encoding
ªª< D
.
ªªD E
UTF8
ªªE I
.
ªªI J
GetBytes
ªªJ R
(
ªªR S
password
ªªS [
)
ªª[ \
)
ªª\ ]
;
ªª] ^
}
ºº 
}
ΩΩ 	
public
øø 
bool
øø  
VerifyPasswordHash
øø &
(
øø& '
string
øø' -
password
øø. 6
,
øø6 7
byte
øø8 <
[
øø< =
]
øø= >

storedHash
øø? I
,
øøI J
byte
øøK O
[
øøO P
]
øøP Q

storedSalt
øøR \
)
øø\ ]
{
¿¿ 	
using
¡¡ 
(
¡¡ 
var
¡¡ 
hmac
¡¡ 
=
¡¡ 
new
¡¡ !
System
¡¡" (
.
¡¡( )
Security
¡¡) 1
.
¡¡1 2
Cryptography
¡¡2 >
.
¡¡> ?

HMACSHA512
¡¡? I
(
¡¡I J

storedSalt
¡¡J T
)
¡¡T U
)
¡¡U V
{
¬¬ 
var
√√ 
computedHash
√√  
=
√√! "
hmac
√√# '
.
√√' (
ComputeHash
√√( 3
(
√√3 4
System
√√4 :
.
√√: ;
Text
√√; ?
.
√√? @
Encoding
√√@ H
.
√√H I
UTF8
√√I M
.
√√M N
GetBytes
√√N V
(
√√V W
password
√√W _
)
√√_ `
)
√√` a
;
√√a b
return
ƒƒ 
computedHash
ƒƒ #
.
ƒƒ# $
SequenceEqual
ƒƒ$ 1
(
ƒƒ1 2

storedHash
ƒƒ2 <
)
ƒƒ< =
;
ƒƒ= >
}
≈≈ 
}
∆∆ 	
public
»» 
void
»» 
EnsureUserHasRole
»» %
(
»»% &
User
»»& *
user
»»+ /
,
»»/ 0
RoleType
»»1 9
requiredRole
»»: F
)
»»F G
{
…… 	
if
   
(
   
user
   
.
   
Role
   
!=
   
requiredRole
   )
)
  ) *
{
ÀÀ 
_logger
ÃÃ 
.
ÃÃ 
Warning
ÃÃ 
(
ÃÃ  
$str
ÕÕ {
,
ÕÕ{ |
user
ŒŒ 
.
ŒŒ 
Email
ŒŒ 
,
ŒŒ 
requiredRole
ŒŒ  ,
,
ŒŒ, -
user
ŒŒ. 2
.
ŒŒ2 3
Role
ŒŒ3 7
)
ŒŒ7 8
;
ŒŒ8 9
throw
œœ 
new
œœ )
UnauthorizedAccessException
œœ 5
(
œœ5 6
$"
œœ6 8
$str
œœ8 ^
{
œœ^ _
requiredRole
œœ_ k
}
œœk l
"
œœl m
)
œœm n
;
œœn o
}
–– 
}
—— 	
private
”” 
string
”” #
GenerateTwoFactorCode
”” ,
(
””, -
)
””- .
{
‘‘ 	
var
’’ 
random
’’ 
=
’’ 
new
’’ 
Random
’’ #
(
’’# $
)
’’$ %
;
’’% &
return
÷÷ 
random
÷÷ 
.
÷÷ 
Next
÷÷ 
(
÷÷ 
$num
÷÷ %
,
÷÷% &
$num
÷÷' -
)
÷÷- .
.
÷÷. /
ToString
÷÷/ 7
(
÷÷7 8
)
÷÷8 9
;
÷÷9 :
}
◊◊ 	
public
ŸŸ 
async
ŸŸ 
Task
ŸŸ 
SendTwoFactorCode
ŸŸ +
(
ŸŸ+ ,
User
ŸŸ, 0
user
ŸŸ1 5
)
ŸŸ5 6
{
⁄⁄ 	
try
€€ 
{
‹‹ 
var
›› 
code
›› 
=
›› #
GenerateTwoFactorCode
›› 0
(
››0 1
)
››1 2
;
››2 3
var
ﬂﬂ 
updatedUser
ﬂﬂ 
=
ﬂﬂ  !
new
ﬂﬂ" %

UserCreate
ﬂﬂ& 0
(
ﬂﬂ0 1
name
ﬂﬂ1 5
:
ﬂﬂ5 6
user
ﬂﬂ7 ;
.
ﬂﬂ; <
Name
ﬂﬂ< @
,
ﬂﬂ@ A
surname
‡‡ 
:
‡‡ 
user
‡‡ !
.
‡‡! "
Surname
‡‡" )
,
‡‡) *

patronymic
·· 
:
·· 
user
··  $
.
··$ %

Patronymic
··% /
,
··/ 0

department
‚‚ 
:
‚‚ 
user
‚‚  $
.
‚‚$ %

Department
‚‚% /
,
‚‚/ 0
email
„„ 
:
„„ 
user
„„ 
.
„„  
Email
„„  %
,
„„% &
passwordHash
‰‰  
:
‰‰  !
user
‰‰" &
.
‰‰& '
PasswordHash
‰‰' 3
,
‰‰3 4
passwordSalt
ÂÂ  
:
ÂÂ  !
user
ÂÂ" &
.
ÂÂ& '
PasswordSalt
ÂÂ' 3
,
ÂÂ3 4
birthDay
ÊÊ 
:
ÊÊ 
user
ÊÊ "
.
ÊÊ" #
BirthDay
ÊÊ# +
,
ÊÊ+ ,
sex
ÁÁ 
:
ÁÁ 
user
ÁÁ 
.
ÁÁ 
Sex
ÁÁ !
,
ÁÁ! "
role
ËË 
:
ËË 
user
ËË 
.
ËË 
Role
ËË #
,
ËË# $
twoFactorCode
ÈÈ !
:
ÈÈ! "
code
ÈÈ# '
,
ÈÈ' (!
twoFactorExpiration
ÍÍ '
:
ÍÍ' (
DateTime
ÍÍ) 1
.
ÍÍ1 2
UtcNow
ÍÍ2 8
.
ÍÍ8 9

AddMinutes
ÍÍ9 C
(
ÍÍC D
$num
ÍÍD F
)
ÍÍF G
)
ÍÍG H
;
ÍÍH I
await
ÏÏ 
_userRepository
ÏÏ %
.
ÏÏ% &
UpdateUserAsync
ÏÏ& 5
(
ÏÏ5 6
user
ÏÏ6 :
.
ÏÏ: ;
Id
ÏÏ; =
,
ÏÏ= >
updatedUser
ÏÏ? J
)
ÏÏJ K
;
ÏÏK L
await
ÓÓ 
_emailService
ÓÓ #
.
ÓÓ# $
SendEmailAsync
ÓÓ$ 2
(
ÓÓ2 3
user
ÓÓ3 7
.
ÓÓ7 8
Email
ÓÓ8 =
,
ÓÓ= >
$str
ÓÓ? H
,
ÓÓH I
$"
ÓÓJ L
$str
ÓÓL Y
{
ÓÓY Z
code
ÓÓZ ^
}
ÓÓ^ _
"
ÓÓ_ `
)
ÓÓ` a
;
ÓÓa b
}
 
catch
ÒÒ 
(
ÒÒ 
	Exception
ÒÒ 
ex
ÒÒ 
)
ÒÒ  
{
ÚÚ 
_logger
ÛÛ 
.
ÛÛ 
Error
ÛÛ 
(
ÛÛ 
ex
ÛÛ  
,
ÛÛ  !
$str
ÛÛ" U
,
ÛÛU V
user
ÛÛW [
.
ÛÛ[ \
Id
ÛÛ\ ^
)
ÛÛ^ _
;
ÛÛ_ `
throw
ÙÙ 
new
ÙÙ "
UserServiceException
ÙÙ .
(
ÙÙ. /
$str
ÙÙ/ H
,
ÙÙH I
ex
ÙÙJ L
)
ÙÙL M
;
ÙÙM N
}
ıı 
}
ˆˆ 	
public
¯¯ 
async
¯¯ 
Task
¯¯ 
<
¯¯ 
bool
¯¯ 
>
¯¯ &
VerifyTwoFactorCodeAsync
¯¯  8
(
¯¯8 9
Guid
¯¯9 =
userId
¯¯> D
,
¯¯D E
string
¯¯F L
code
¯¯M Q
)
¯¯Q R
{
˘˘ 	
try
˙˙ 
{
˚˚ 
var
¸¸ 
user
¸¸ 
=
¸¸ 
await
¸¸  
_userRepository
¸¸! 0
.
¸¸0 1
GetUserByIdAsync
¸¸1 A
(
¸¸A B
userId
¸¸B H
)
¸¸H I
;
¸¸I J
if
˝˝ 
(
˝˝ 
user
˝˝ 
==
˝˝ 
null
˝˝  
)
˝˝  !
{
˛˛ 
_logger
ˇˇ 
.
ˇˇ 
Warning
ˇˇ #
(
ˇˇ# $
$str
ˇˇ$ F
,
ˇˇF G
userId
ˇˇH N
)
ˇˇN O
;
ˇˇO P
throw
ÄÄ 
new
ÄÄ #
UserNotFoundException
ÄÄ 3
(
ÄÄ3 4
$"
ÄÄ4 6
$str
ÄÄ6 C
{
ÄÄC D
userId
ÄÄD J
}
ÄÄJ K
$str
ÄÄK U
"
ÄÄU V
)
ÄÄV W
;
ÄÄW X
}
ÅÅ 
if
ÉÉ 
(
ÉÉ 
user
ÉÉ 
.
ÉÉ 
TwoFactorCode
ÉÉ &
!=
ÉÉ' )
code
ÉÉ* .
||
ÉÉ/ 1
user
ÉÉ2 6
.
ÉÉ6 7!
TwoFactorExpiration
ÉÉ7 J
<
ÉÉK L
DateTime
ÉÉM U
.
ÉÉU V
UtcNow
ÉÉV \
)
ÉÉ\ ]
{
ÑÑ 
return
ÖÖ 
false
ÖÖ  
;
ÖÖ  !
}
ÜÜ 
var
àà 
updatedUser
àà 
=
àà  !
new
àà" %

UserCreate
àà& 0
(
àà0 1
name
àà1 5
:
àà5 6
user
àà7 ;
.
àà; <
Name
àà< @
,
àà@ A
surname
ââ 
:
ââ 
user
ââ !
.
ââ! "
Surname
ââ" )
,
ââ) *

patronymic
ää 
:
ää 
user
ää  $
.
ää$ %

Patronymic
ää% /
,
ää/ 0

department
ãã 
:
ãã 
user
ãã  $
.
ãã$ %

Department
ãã% /
,
ãã/ 0
email
åå 
:
åå 
user
åå 
.
åå  
Email
åå  %
,
åå% &
passwordHash
çç  
:
çç  !
user
çç" &
.
çç& '
PasswordHash
çç' 3
,
çç3 4
passwordSalt
éé  
:
éé  !
user
éé" &
.
éé& '
PasswordSalt
éé' 3
,
éé3 4
birthDay
èè 
:
èè 
user
èè "
.
èè" #
BirthDay
èè# +
,
èè+ ,
sex
êê 
:
êê 
user
êê 
.
êê 
Sex
êê !
,
êê! "
role
ëë 
:
ëë 
user
ëë 
.
ëë 
Role
ëë #
,
ëë# $
twoFactorCode
íí !
:
íí! "
null
íí# '
,
íí' (!
twoFactorExpiration
ìì '
:
ìì' (
null
ìì) -
)
ìì- .
;
ìì. /
await
ïï 
_userRepository
ïï %
.
ïï% &
UpdateUserAsync
ïï& 5
(
ïï5 6
user
ïï6 :
.
ïï: ;
Id
ïï; =
,
ïï= >
updatedUser
ïï? J
)
ïïJ K
;
ïïK L
return
óó 
true
óó 
;
óó 
}
òò 
catch
ôô 
(
ôô #
UserNotFoundException
ôô (
)
ôô( )
{
öö 
throw
õõ 
;
õõ 
}
úú 
catch
ùù 
(
ùù 
	Exception
ùù 
ex
ùù 
)
ùù  
{
ûû 
_logger
üü 
.
üü 
Error
üü 
(
üü 
ex
üü  
,
üü  !
$str
üü" X
,
üüX Y
userId
üüZ `
)
üü` a
;
üüa b
throw
†† 
new
†† "
UserServiceException
†† .
(
††. /
$str
††/ J
,
††J K
ex
††L N
)
††N O
;
††O P
}
°° 
}
¢¢ 	
}
££ 
}§§ Í7
/Users/m4ks0n/study/IU7/sem7/testing/bmstu-iu7-sem7-web/src/ProdMonitor/ProdMonitor.Application/Services/AssemblyLineService.cs
	namespace 	
ProdMonitor
 
. 
Application !
.! "
Services" *
{		 
public

 

class

 
AssemblyLineService

 $
:

% & 
IAssemblyLineService

' ;
{ 
private 
readonly #
IAssemblyLineRepository 0#
_assemblyLineRepository1 H
;H I
private 
readonly 
ILogger  
_logger! (
;( )
public 
AssemblyLineService "
(" ##
IAssemblyLineRepository# :"
assemblyLineRepository; Q
,Q R
ILogger 
logger 
) 
{ 	#
_assemblyLineRepository #
=$ %"
assemblyLineRepository& <
;< =
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
AssemblyLine &
>& '#
CreateAssemblyLineAsync( ?
(? @
AssemblyLineCreate@ R
lineS W
)W X
{ 	
try 
{ 
_logger 
. 
Information #
(# $
$str$ H
,H I
lineJ N
.N O
NameO S
)S T
;T U
return 
await #
_assemblyLineRepository 4
.4 5#
CreateAssemblyLineAsync5 L
(L M
lineM Q
)Q R
;R S
} 
catch 
( 
	Exception 
ex 
)  
{ 
_logger 
. 
Error 
( 
ex  
,  !
$str" N
,N O
lineP T
.T U
NameU Y
)Y Z
;Z [
throw   
new   (
AssemblyLineServiceException   6
(  6 7
$str  7 N
,  N O
ex  P R
)  R S
;  S T
}!! 
}"" 	
public$$ 
async$$ 
Task$$ 
<$$ 
List$$ 
<$$ 
AssemblyLine$$ +
>$$+ ,
>$$, -$
GetAllAssemblyLinesAsync$$. F
($$F G
AssemblyLineFilter$$G Y
filter$$Z `
)$$` a
{%% 	
try&& 
{'' 
_logger(( 
.(( 
Information(( #
(((# $
$str(($ V
,((V W
filter((X ^
)((^ _
;((_ `
var)) 
result)) 
=)) 
await)) "#
_assemblyLineRepository))# :
.)): ;$
GetAllAssemblyLinesAsync)); S
())S T
filter))T Z
)))Z [
;))[ \
if** 
(** 
!** 
result** 
.** 
Any** 
(**  
)**  !
)**! "
{++ 
_logger,, 
.,, 
Warning,, #
(,,# $
$str,,$ S
,,,S T
filter,,U [
),,[ \
;,,\ ]
throw-- 
new-- !
LineNotFoundException-- 3
(--3 4
$str--4 D
)--D E
;--E F
}.. 
return00 
result00 
;00 
}11 
catch22 
(22 !
LineNotFoundException22 (
)22( )
{33 
throw44 
;44 
}55 
catch66 
(66 
	Exception66 
ex66 
)66  
{77 
_logger88 
.88 
Error88 
(88 
ex88  
,88  !
$str88" @
)88@ A
;88A B
throw99 
new99 (
AssemblyLineServiceException99 6
(996 7
$str997 L
,99L M
ex99N P
)99P Q
;99Q R
}:: 
};; 	
public== 
async== 
Task== 
<== 
AssemblyLine== &
>==& '$
GetAssemblyLineByIdAsync==( @
(==@ A
Guid==A E
id==F H
)==H I
{>> 	
try?? 
{@@ 
_loggerAA 
.AA 
InformationAA #
(AA# $
$strAA$ I
,AAI J
idAAK M
)AAM N
;AAN O
varBB 
assemblyLineBB  
=BB! "
awaitBB# (#
_assemblyLineRepositoryBB) @
.BB@ A$
GetAssemblyLineByIdAsyncBBA Y
(BBY Z
idBBZ \
)BB\ ]
;BB] ^
ifCC 
(CC 
assemblyLineCC  
==CC! #
nullCC$ (
)CC( )
{DD 
_loggerEE 
.EE 
WarningEE #
(EE# $
$strEE$ J
,EEJ K
idEEL N
)EEN O
;EEO P
throwFF 
newFF !
LineNotFoundExceptionFF 3
(FF3 4
$"FF4 6
$strFF6 C
{FFC D
idFFD F
}FFF G
$strFFG Q
"FFQ R
)FFR S
;FFS T
}GG 
returnII 
assemblyLineII #
;II# $
}JJ 
catchKK 
(KK !
LineNotFoundExceptionKK (
)KK( )
{LL 
throwMM 
;MM 
}NN 
catchOO 
(OO 
	ExceptionOO 
exOO 
)OO  
{PP 
_loggerQQ 
.QQ 
ErrorQQ 
(QQ 
exQQ  
,QQ  !
$strQQ" M
,QQM N
idQQO Q
)QQQ R
;QQR S
throwRR 
newRR (
AssemblyLineServiceExceptionRR 6
(RR6 7
$strRR7 K
,RRK L
exRRM O
)RRO P
;RRP Q
}SS 
}TT 	
publicVV 
TaskVV #
DeleteAssemblyLineAsyncVV +
(VV+ ,
GuidVV, 0
idVV1 3
)VV3 4
{WW 	
tryXX 
{YY 
_loggerZZ 
.ZZ 
InformationZZ #
(ZZ# $
$strZZ$ J
,ZZJ K
idZZL N
)ZZN O
;ZZO P
return[[ #
_assemblyLineRepository[[ .
.[[. /#
DeleteAssemblyLineAsync[[/ F
([[F G
id[[G I
)[[I J
;[[J K
}\\ 
catch]] 
(]] !
LineNotFoundException]] (
)]]( )
{^^ 
throw__ 
;__ 
}`` 
catchaa 
(aa 
	Exceptionaa 
exaa 
)aa  
{bb 
_loggercc 
.cc 
Errorcc 
(cc 
excc  
,cc  !
$strcc" P
,ccP Q
idccR T
)ccT U
;ccU V
throwdd 
newdd (
AssemblyLineServiceExceptiondd 6
(dd6 7
$strdd7 N
,ddN O
exddP R
)ddR S
;ddS T
}ee 
}ff 	
}gg 
}hh 