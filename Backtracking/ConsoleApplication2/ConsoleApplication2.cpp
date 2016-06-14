// ConsoleApplication2.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include "conio.h"

using namespace std;

int psolutie[10000],orase[10000] ,n; //orase este vectorul care contine obiectivele fiecarui oras, prima pozitie primul oras, pozitia 2 orasul 2....
int x, y, p , s1, s2 ;   // s1 = suma obiectivelor pana la jumatatea traseului , s2= suma obictivelor de la jumatatea traseului pana la finalul lui;
int poz;   // In vectorul psolutie retin cate o permutare a ordinei de parcurgere a oraselor. Vectorul orase va contine numarul de obiective al fiecarui oras.
// Cand calculez suma obiectivelor traseului pana la jumatatea lui voi aduna la suma in ordine crescatoare obiectivele orasului cu poz = psolutie[i]
// suma = suma + orase[poz];
///functie de tiparire
void tipar(int k)
{
	for (int i = 1; i <= k; i++)
	{
		poz = psolutie[i];
		cout <<"Numarul orasului  "<<poz << ' ' <<endl; // voi afisa numarul orasului , urmat de obiectivele sale
		cout <<"Numarul de obiective al orasului "<< orase[poz] << ' '<<endl; 

	}
	cout << endl;
}

/// functie de validare
/// daca psolutie posibila solutie indeplineste conditiile atunci se va folosii functia de tiparire pentru a o afisa
int valid(int k)
{
	for (int i = 1; i <= k -1; i++)
	{
		//cout << st[i] << ' ';
		//cout << endl;
		//cout << st[k] << ' ';
		//cout << endl;
		if (psolutie[i] == psolutie[k])  /// verificare daca psolutie trece o singura data prin fiecare oras
			return 0;
		s1 = 0;
		s2 = 0;
		for (int i = 1; i <= k / 2; i++)
		{// suma primei jumatati
			poz = psolutie[i];
			s1 = s1 + orase[poz];
		}
		for (int i =( k/2)+1; i <= k; i++)
		{// si suma celei de a doua jumatati
			poz = psolutie[i];
			s2 = s2 + orase[poz];
		}
		
		if (s1 <= s2)    // s1 > s2 atunci return 1, negam pentru a returna fals;
			return 0;
		if (s1 + s2 < p)  // daca s1 + s2 => p atunci return 1, negam pentru a returna fals  ;
			return 0;
	}
	return 1;
}
/// k este dimensiunea posibilei solutii
/// psolutie este vectorul care contine posibila solutie pentru parcurgerea traseului
void back(int k,int x ,int y)
{
	for (int i = 1; i <= n; i++)
	{
		psolutie[k] = i;
		
		if (valid(k))
		{
			
			if ((k >=x) && (k <= y))  /// verificare daca psolutie are cel putin x orase vizitate, si maxim y orase vizitate
			{
				tipar(k); /// afiseaza  una dintre solutii
				back(k + 1,x,y);
			}
			else
				back(k + 1,x,y);
		}
	}
}

/// n este numarul de orase
/// k este dimensiunea posibilei solutii
/// x < y altfel programul nu va functiona conform cerintelor
/// p>= 0
int main()
{
	cout << "Dati numarul de orase, n= ";
	cin >> n;
	
	for (int i = 1; i <=n; i++)
	{
		cout << " Dati numarul de obiective al fiecarui oras: "; // primul oras se afla pe pozitia 1, al doilea pe pozitia 2 ....
		cin >> orase[i];		
	}
	cout << " Dati numarul minim de orase care trebuiesc parcurse x= ";
	cin >> x; 
	cout << " Dati numarul maxim de orase care trebuiesc parcurse y= ";
	cin >> y;
	cout << " Dati numarul minim de obiective care trebuiesc vizitate p=  ";
	cin >> p; 
	back(1,x,y);
    return 0;

}

// caz de testare n=3 , x=2, y=3 ,p=4  =>   orasele : 21 si 23  
  // orasul 1 are 2 obiective
//orasul 2 are 3 obiective
//orasul 3 are 2 obiective