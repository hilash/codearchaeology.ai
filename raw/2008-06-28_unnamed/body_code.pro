% Prolog Project - Hila Shmuel, Yod Alef 1.
% Countries: 12

%country/6.
%country(name(_name,_capital),_population,_area,_GDP,_continent,_president);

% area in km^2
%GPD(=PPP) in billions
% president can represent the king, prime minister - the person who is
% most recognized with the country leadership; 

country(name(israel,jerusalem),7282000,22072,185.8,asia,ehud_olmert).
country(name(usa,washington),304072000,9826630,13543,america,george_w_bush).
country(name(iran,tehran),71208000,1648195,278,asia,mahmoud_ahmadinejad).
country(name(jordan,amman),6053193,89342,27.96,asia,abdullah_ii).
country(name(lebanon,beirut),4196453,10452,41.96,asia,fouad_siniora).
country(name(syria,damascus),19405000,185180,71.74,asia,bashar_al_assad).
country(name(egypt,cairo),80335036,980869,329.791,africa,hosni_mubarak).
country(name(iraq,baghdad),29267000,438317 ,89.8,asia,jalal_talabani).
country(name(brazil,brasilia),186757608,8514877,1804,america,luiz_inacio_lula_da_silva).
country(name(france,paris),64473140,674843,1871,europe,nicolas_sarkozy).
country(name(spain,madrid),45200737,504030,1310,europe,juan_carlos_i).
country(name(mexico,mexico_city),108700891,1972550,840.012,america,felipe_calderon).

%enemies/2.
%enemies(_name1,[_n1,_n2,_n3]);

enemies(israel,[iran,lebanon,syria,iraq]).
enemies(usa,[iran,iraq]).
enemies(iran,[israel,usa]).
enemies(lebanon,[israel]).
enemies(syria,[israel]).
enemies(iraq,[israel,usa]).

%neigbours/2.
%neigbours(_name1,[n1,_n2,_3]);

neigbours(israel,[jordan,lebanon,syria,egypt]).
neigbours(usa,[mexico]).
neigbours(iran,[iraq]).
neigbours(jordan,[syria,iraq,israel]).
neigbours(lebanon,[israel,syria]).
neigbours(syria,[jordan,lebanon,iraq,israel]).
neigbours(egypt,[israel]).
neigbours(iraq,[jordan,syria,iran]).
neigbours(france,[spain]).
neigbours(spain,[france]).
neigbours(mexico,[usa]).

% ------------------------------------------------------------------------
% RULES;

%BASIC RULES ABOUT COUNTRY:
%country(name(_name,_capital),_population,_area,_GDP,_continent,_president);
country_capital(_count,_cap):-country(name(_count,_cap),_,_,_,_,_).
country_population(_count,_pop):-country(name(_count,_),_pop,_,_,_,_).
country_area(_count,_area):-country(name(_count,_),_,_area,_,_,_).
country_GDP(_count,_gdp):-country(name(_count,_),_,_,_gdp,_,_).
country_continent(_count,_conti):-country(name(_count,_),_,_,_,_conti,_).
country_president(_count,_pres):-country(name(_count,_),_,_,_,_,_pres).

%OTHER RULES:

is_country(_country1):-country(name(_country1,_),_,_,_,_,_).
is_enemy(_country1,_country2):-enemies(_country1,L),member(_country2,L).
is_neigbour(_country1,_country2):-neigbours(_country1,L),member(_country2,L).

max_population(_country1,_pop1):-country_population(_country1,_pop1),
	not((country_population(_country2,_pop2),_country1\=_country2,_pop2>_pop1)).
max_area(_country1,_pop1):-country_area(_country1,_pop1),
	not((country_area(_country2,_pop2),_country1\=_country2,_pop2>_pop1)).
max_GDP(_country1,_pop1):-country_GDP(_country1,_pop1),
	not((country_GDP(_country2,_pop2),_country1\=_country2,_pop2>_pop1)).

min_population(_country1,_pop1):-country_population(_country1,_pop1),
	not((country_population(_country2,_pop2),_country1\=_country2,_pop2<_pop1)).
min_area(_country1,_pop1):-country_area(_country1,_pop1),
	not((country_area(_country2,_pop2),_country1\=_country2,_pop2<_pop1)).
min_GDP(_country1,_pop1):-country_GDP(_country1,_pop1),
	not((country_GDP(_country2,_pop2),_country1\=_country2,_pop2<_pop1)).

negotiator(_country1,_country2,_nego):-is_country(_nego),is_enemy(_country1,_country2),_nego\=_country1,
	_nego\=_country2,not((is_enemy(_nego,_country1);is_enemy(_nego,_country2))).

neigbour_n_enemy(_country1,_country2):-is_enemy(_country1,_country2),is_neigbour(_country1,_country2).