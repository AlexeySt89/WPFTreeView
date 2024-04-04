using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WPFTreeView.Data
{
    public static class DataWorker
    {
        public static List<Model.Object> GetObjects()
        {
            List<Model.Object> objects = new List<Model.Object>();

            using (var dbContext = new ApplicationDbContext())
            {
                var objectsFromDb = dbContext.Objects.Include(obj => obj.Attributes).ToList();

                foreach (var obj in objectsFromDb)
                {
                    var objModel = new Model.Object
                    {
                        Id = obj.Id,
                        Type = obj.Type,
                        Product = obj.Product,
                        Attributes = new ObservableCollection<Model.Attribute>(obj.Attributes.Select(attr => new Model.Attribute
                        {
                            ObjectId = attr.ObjectId,
                            Name = attr.Name,
                            Value = attr.Value
                        }))
                    };
                    objects.Add(objModel);
                }
            }

            return objects;
        }

        public static List<Model.Attribute> GetAttributesForObject()
        {
            List<Model.Attribute> attributes = new List<Model.Attribute>();

            using (var dbContext = new ApplicationDbContext())
            {
                var attributesFromDb = dbContext.Attributes.ToList();

                foreach (var attr in attributesFromDb)
                {
                    var attributeModel = new Model.Attribute
                    {
                        ObjectId = attr.ObjectId,
                        Name = attr.Name,
                        Value = attr.Value
                    };
                    attributes.Add(attributeModel);
                }
            }
            return attributes;
        }

        public static void SaveToJson<T>(T data)
        {
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(@"path", json);
        }

        public static string CreateObject(string type, string product)
        {
            string result = "Уже существует";
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                bool checkIsExist = db.Objects.Any(o => o.Type == type && o.Product == product);
                if (!checkIsExist)
                {
                    Model.Object obj = new Model.Object() { Type = type, Product = product };
                    db.Objects.Add(obj);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        public static string CreateAttribute(string name, string value, Model.Object obj)
        {
            string result = "Уже существует";
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                bool checkIsExist = db.Attributes.Any(a => a.Name == name && a.Value == value);
                if (!checkIsExist)
                {
                    Model.Attribute attribute = new Model.Attribute() { Name = name, Value = value, ObjectId = obj.Id };
                    db.Attributes.Add(attribute);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        public static string DeleteObject(Model.Object obj)
        {
            string result = "Такого объекта не существует";
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Objects.Remove(obj);
                db.SaveChanges();
                result = "Объект " + obj.Type + " удален";
            }
            return result;
        }

        public static string DeleteAttribute(Model.Attribute attribute)
        {
            string result = "Такого атрибута не существует";
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Attributes.Remove(attribute);
                db.SaveChanges();
                result = "Атрбиут " + attribute.Name + " удален";
            }
            return result;
        }

    }
}
