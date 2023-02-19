
Nous avons décidé d'utiliser l'architecture hexagonale pour notre application.
Cette architecture permet de séparer les différentes couches de l'application de manière claire et
de faciliter l'ajout de nouvelles fonctionnalités sans impacter les autres parties de l'application.
L'architecture hexagonale est également facilement testable et modifiable.

En utilisant l'architecture hexagonale, nous allons devoir mettre en place une structure claire pour
les différentes couches de l'application (domaine, infrastructure, interface utilisateur, etc).

La classe HandlersProcessor permet à l'infrastrucure d'exécuter des commandes ou queries, sans avoir
besoin de la référence à la couche de l'application. Cela permet de séparer les différentes couches.
