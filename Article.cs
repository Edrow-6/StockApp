namespace StockApp // Répertoire de Travail
{
    class Article // Utilisation de la classe Article pour tout le monde
    {
        private int _number; // Typage ou type de la variable | Attributs ou propriétés de l'objet Article
        private string _name;
        private float _price;
        private int _quantity;

        public int Number // Convention de nomage | n minuscule pour attributs (var) | N majuscule pour méthode get/set
        {
            get { return _number; } // Retourne la valeur de l'objet (output)
            set { _number = value; } // Définit une valeur dans un atrribut d'un objet (input)
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public float Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        // Le Constructeur
        public Article(int number, string name, float price, int quantity) // 4 Arguments définis dans le constructeur
        {
            Number = number; // (this.valeur) veut dire de prendre les attributs de la classe
            Name = name; // La valeur "name" est enregistrée dans l'attribut "Name"
            Price = price;
            Quantity = quantity;
        }

        public override string ToString() // Création d'une fonction "toString()" | Le override précise au système qu'il doit utiliser la fonction que j'ai moi-même codé
        {
            return $"N°{Number}, {Name}, {Price}, {Quantity}";
        }

    }
}
