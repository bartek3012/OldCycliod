using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Backend.Menager
{
    public abstract class BaseMenager<T> where T : BaseEntity
    {
        public List<T> Elements { get; set; }
        public BaseMenager()
        {
            Elements = new List<T>();
            Initialize();
        }

        protected abstract void Initialize();
        public void Add(BaseEntity entity)
        {
            Elements.Add(entity as T);
        }
        public BaseEntity GetByType(string type)
        {
            try
            {
                return Elements.First(p => p.Type == type);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Błąd wyszukiwania atrybutu z listy o nazwie!");
                return null;
            }
        }



    }
}
