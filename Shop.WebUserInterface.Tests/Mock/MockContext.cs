﻿using Shop.Core.Logique;
using Shop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.WebUserInterface.Tests.Mock
{
    public class MockContext<T> : IRepository<T> where T : BaseEntity
    {
  
        List<T> items;
        string className;

        public MockContext()
        {
            className = typeof(T).Name;
   
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void SaveChanges()
        {
            return;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T prodToUpdate = items.Find(prod => prod.Id == t.Id);
            if (prodToUpdate != null)
            {
                prodToUpdate = t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public T FindById(int id)
        {
            T t = items.Find(prod => prod.Id == id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        //Le type IQuerable accépte les requête LINQ contrairement à une list classique

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(int id)
        {
            T ProdToDelete = items.Find(p => p.Id == id);
            if (ProdToDelete != null)
            {
                items.Remove(ProdToDelete);
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
    }
}
