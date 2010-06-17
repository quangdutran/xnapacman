#include<iostream.h>
#include<conio.h>
#include<stdlib.h>
#include<dos.h>
int losowanie();
int i=0,j=0,tab[6],k=0,il, los,skrl,skrlrecz,skrlchyb;
int zak[5];
int duzy[5][6]/*={{1,2,3,4,5,6},{7,8,9,10,11,12},{13,14,15,16,17,18},{19,20,21,22,23,24},{25,26,27,28,29,30}}*/,expres[5][5],multi[5][20];
int kuponduzy, kuponexpres,kuponmulti;
void main()
{
do{
clrscr();
tab[0]=losowanie();
cout<<"\n\t";
textcolor(14);
cprintf("1");
textcolor(15);
cprintf(". Losowanie \"Du¾ego Lotka\"");
cout<<"\n\t";
textcolor(14);
cprintf("2");
textcolor(15);
cprintf(". Losowanie \"Expres Lotka\"");
cout<<"\n\t";
textcolor(14);
cprintf("3");
textcolor(15);
cprintf(". Losowanie \"Multi Lotka\"");
cout<<"\n\t";
textcolor(14);
cprintf("4");
textcolor(15);
cprintf(". Skre˜lanie kuponu");
cout<<"\n\t";
textcolor(14);
cprintf("5");
textcolor(15);
cprintf(". Wyj˜cie");

do{los=getch();}
while(los<49||los>53);
switch(los)
 {
 case '1':
 clrscr();
 if(kuponduzy)
 {textcolor(15);
 textbackground(0);
 cprintf("Tw¢j kupon: ");
 j=0;
 do{cout<<"\n\t"<<j+1<<". ";
    i=0;
    do{cout<<duzy[j][i];i++;
       if(i!=6){cout<<", ";}
      }
    while(i!=6);
    j++;
   }
 while(j!=5);
 i=0;
  do{ randomize();
     tab[i]=losowanie();j=0;
     while(j!=i||tab[i]==0)
          {if(tab[i]==tab[j]||tab[i]==0)
             {i--;j=i-1;}
              j++;
          }
     i++;
    }
  while(i!=6);
 cout<<"\n\nOto wylosowane liczby: ";
 i=0;
 do{cout<<tab[i];if(i!=5){cout<<", ";}i++;}
 while(i!=6);
 i=j=k=0;
 do{zak[i]=0;i++;}
 while(i!=5);
 i=0;
 do{
    if(duzy[j][i]==tab[k]){zak[j]++;}
    k++;
    if(k==6){i++;k=0;}
    if(i==6){j++;i=0;}
   }
 while(j!=6);
 i=0;
 cout<<"\n\n";
 do{cout<<"\t"<<i+1<<". "<<zak[i]<<" trafionych\n";i++;}
 while(i!=5);
 }
 else{cout<<"Nie masz skre˜lonego kuponu do Du¾ego Lotka.";}
 getch();
 break;

 case '2':
 clrscr();
 if(kuponexpres)
 {
 textcolor(15);
 textbackground(0);
 cprintf("Tw¢j kupon: ");
 j=0;
 do{cout<<"\n\t"<<j+1<<". ";
    i=0;
    do{cout<<expres[j][i];i++;
       if(i!=5){cout<<", ";}
      }
    while(i!=5);
    j++;
   }
 while(j!=5);
 }
 else{cout<<"Nie masz skre˜lonego kuponu do Expres Lotka.";}
 getch();
 break;

 case '3':
 clrscr();
 if(kuponmulti)
 {
 textcolor(15);
 textbackground(0);
 cprintf("Tw¢j kupon: ");
 j=0;
 do{cout<<"\n\t"<<j+1<<". ";
    i=0;
    do{cout<<multi[j][i];i++;
       if(i!=20){cout<<", ";}
      }
    while(i!=20);
    j++;
   }
 while(j!=5);
 }
 else{cout<<"Nie masz skre˜lonego kuponu do Multi Lotka.";}
 getch();
 break;

 case '4':
 clrscr();
 textcolor(14);
 cout<<"\n\t";
 cprintf("1");
 textcolor(15);
 cprintf(". Skre˜lanie r©czne\r");
 textcolor(14);
 cout<<"\n\t";
 cprintf("2");
 textcolor(15);
 cprintf(". Chybiˆ - trafiˆ\n");
 do{skrl=getch();}
 while(skrl<49||skrl>50);
       switch (skrl)
       {
        case '1':
        clrscr();
        cout<<"Jaki kupon skre˜lamy?\n\t";
        textcolor(14);
        cprintf("1");
        textcolor(15);
        cprintf(". Du¾ego Lotka");
        cout<<"\n\t";
        textcolor(14);
        cprintf("2");
        textcolor(15);
        cprintf(". Expres Lotka");
        cout<<"\n\t";
        textcolor(14);
        cprintf("3");
        textcolor(15);
        cprintf(". Multi Lotka");
        do{skrlrecz=getch();}
        while(skrlrecz<49||skrlrecz>51);
        switch(skrlrecz)
              {
               case '1':
               j=0;
               do{clrscr();i=0;
                  cout<<"Dla "<<j+1<<" zakˆadu\n";
                  do{ cout<<"podaj "<<i+1<<" liczb©:\t";
                      cin>>duzy[j][i];i++;
                    }
                  while(i!=6);j++;
                 }
               while(j!=5);
               kuponduzy=1;
               break;

               case '2':
               j=0;
               do{clrscr();i=0;
                  cout<<"Dla "<<j+1<<" zakˆadu\n";
                  do{ cout<<"podaj "<<i+1<<" liczb©:\t";
                      cin>>expres[j][i];i++;
                    }
                  while(i!=5);j++;
                 }
               while(j!=5);
               kuponexpres=1;
               break;

               case '3':
               j=0;
               do{clrscr();i=0;
                  cout<<"Dla "<<j+1<<" zakˆadu\n";
                  do{ cout<<"podaj "<<i+1<<" liczb©:\t";
                      cin>>multi[j][i];i++;
                    }
                  while(i!=20);j++;
                 }
               while(j!=5);
               kuponmulti=1;
               break;
               default : ;
              }
        break;

        case '2':
        clrscr();
        cout<<"W co b©dziesz graˆ?\n\t";
        textcolor(14);
        cprintf("1");
        textcolor(15);
        cprintf(". Du¾y Lotek");
        cout<<"\n\t";
        textcolor(14);
        cprintf("2");
        textcolor(15);
        cprintf(". Expres Lotek");
        cout<<"\n\t";
        textcolor(14);
        cprintf("3");
        textcolor(15);
        cprintf(". Multi Lotek");
        do{skrlchyb=getch();}
        while(skrlchyb<49||skrlchyb>51);
              switch(skrlchyb)
                    {
                     case'1':
                     clrscr();
                     k=0;
                     do{i=0;
                        do{ randomize();
                           duzy[k][i]=losowanie();j=0;
                           while(j!=i||duzy[k][i]==0)
                                {if(duzy[k][i]==duzy[k][j]||duzy[k][i]==0)
                                    {i--;j=i-1;}
                                 j++;
                                }
                           i++;
                          }
                        while(i!=6);k++;
                       }
                     while(k!=5);
                     cout<<"Tw¢j kupon: ";
                     j=0;
                     do{cout<<"\n\t"<<j+1<<". ";
                        i=0;
                        do{cout<<duzy[j][i];i++;
                        if(i!=6){cout<<", ";}
                          }
                        while(i!=6);
                        j++;
                       }
                     while(j!=5);
                     kuponduzy=1;
                     getch();
                     break;
                     case'2':
                     break;
                     case'3':
                     break;
                     default: ;
                    }
        break;
        default : ;
       }
 break;

 }
}
while(los!=53);
/*
cout<<los;
cout<<"Jak¥ liczbe losowaä wykona†: ";
cin>>il;
randomize();

i=0;
do{
tab[i]=random(50);j=0;
  while(j!=i||tab[i]==0)
  {if(tab[i]==tab[j]||tab[i]==0)
       {i--;j=i-1;}
       j++;
  }
  i++;
  }
while(i!=6);
i=0;
cout<<" Losowanie nr "<<k+1<<":";
do{
cout<<"\t\b\b"<<tab[i]<<",";
  i++;
  }
while (i!=6);
cout<<endl;
*/
clrscr();
cout<<"\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\t\t\tÉÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍ»\n"
<<"\t\t\tº Program napisaˆ:          º\n"
<<"\t\t\tº  Krzysztof Szumny - Noisy º\n"
<<"\t\t\tº  noisy@autograf.pl        º\n"
<<"\t\t\tº  gg: 1391200              º\n"
<<"\t\t\tÈÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍÍ¼\n";
delay(2000);


}
/*******************************************************************/
int potega(int podstawa, int wykladnik)
{int i;
int pot;
for((pot=1,i=0); i!=wykladnik; i++)
    {pot=pot*podstawa;
        }
 return pot;
}
/*******************************************************************/
/****************************************************************************/
int losowanie()
{int tab;
randomize();
   do{
      tab=random(50);
     }
   while(tab==0);
return tab;

}
/****************************************************************************/
