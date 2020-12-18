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
    }
}
