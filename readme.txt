Memor'i


16/10/2021 - 
Premier commit qui propose un début d'architecture du projet, 
avec un coeur dédié a l'affichagen et un autre aux entrées utilisateurs. 

Les objets 'Card', 'Map' (qui deviendra CardManager) sont présents, mais assez basiques. 


17/10/2021 - 

Début du travail sur les animations, avec pour objectif de proposer une animation 3D.
Quelques tentatives d'optimisation sur l'affichage. 


14/11/2021- 

Beaucoup de temps investi sur une proposition d'affichage (qui ne sera pas retenue). 
Orgnaisation du projet, pour que ça reste un minimum potable. Implémentation des cartes animées
en temps réel. De nombreux problèmes d'optimisation de l'affichage m'ont poussés a abandonner les
console.write. Remplacement par la classe FastConsole, qui permet d'écrire bien plus efficacement et
fiablement que les méthodes précédentes. Charge côté coeur de draw augmentée, mais devient plus constante
 - moins de chute de fps. 


15/11/2021 -

Après pas mal de clean, amélioration des animations en les implémentants dans la nouvelle console (fastConsole). 
Abandon du système d'affichage qui avait été imaginé auparavant : la nouvelle console est très rapide. 
Optimisation sur cette dernière, pour pouvoir afficher plus de charactères simultanéments.
Implémentation des charactères colorés - début des cartes entièrement en couleur. 

Ces cartes sont très lourdes a charger. Créer un écran de chargement ? 


22/11/2021 - 

Passages des cartes au charactères colorés, implémenation dans la fastconsole de la couleur de background. 
Créations de fonctions utilitaires, et de lecture des fichiers de cartes de couleurs. 


26/11/2021 - 

Quelques corrections, et ajout des premières cartes


2/12/2021 - 

Séparation du chargement des cartes dans une classe dédiée. Implémentation des animations dans chaque carte. 
Amélioration du placement automatique des cartes. 
Création de la barre de chargement au début du projet, a cause du nombre croissant de cartes qui augmente les temps de chargements. 
Les temps de chargements ressemblent a des moments ou l'application de répond plus :
Trouver une animation de chargement ? 
Optimisations de l'affichage, pour gagner encore en vitesse. 


11/12/2021 - 

Création des animCard, qui sont pré-calculés en très haute définition. Création de l'écran de chargement inter-jeux, en utilisant
ces animcards. Grace a leur valeur calculées au chargement du projet, et a d'autres optimisations apportées a l'affichage, 
ces animations semble relativement fluide, même en très haute qualité. 
Créations des menus en ré-utilisants le système de cartes. 

Créations des scoped timers, très utiles pour connaitre le temps d'execution d'une fonction. 
Ajout de nombreuses cartes. 


13/12/2021 - 
Création du premier jeu (enfin) en se basant sur tous les systèmes mis en place auparavant. Très rapide a designer et a mettre en pratique. 
Implémentation du multijoueur, et du tableau des scores, ainsi que l'affichage de ces derniers en partie. Ajout de cartes et menus. 


15/12/2021 - 

Ajout de la gestions des touches. Amélioration et créations de nombreux menus. 
Fixed really weird bugs.
Création du deuxième jeu. 
Tired. 

