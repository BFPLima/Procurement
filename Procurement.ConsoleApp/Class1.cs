using Procurement.Model.Entities;
using Procurement.Model.Users;
using Procurement.Repository.EF;
using Procurement.Repository.EF.Entities;
using Procurement.Repository.EF.Users;
using Procurement.Repository.Interface.Entities;
using Procurement.Repository.Interface.Users;
using Procurement.Service.Interface.Entities;
using Procurement.Service.Interface.Users;
using Procurement.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.ConsoleApp
{
    class Class1
    {
        public static void Main()
        {
            ProcurementContext db = new ProcurementContext();


            UnitOfWorkProcurement unit = new UnitOfWorkProcurement(db);
           // InitialSeed(db,unit);

            IRepositoryItem rep = new RepositoryItem(db, unit);
            IRepositoryAttributeValue repAV = new RepositoryAttributeValue(db, unit);
            IRepositoryCustumerOrderItem repositoryCustumerOrderItem = new RepositoryCustumerOrderItem(db, unit);
            IRepositorySupplierOffer repositorySupplierOffer = new RepositorySupplierOffer(db, unit);


            var items = rep.Queryable().ToList();

            int count = 1;
            foreach (var item in items)
            {
                item.UNSPSC = item.UNSPSC + "-" + count.ToString().PadLeft(4, '0');
                rep.Update(item);
                count++;
            }
            unit.SaveChanges();

            //Item i = rep.Find(781);
            //var ass = repositoryCustumerOrderItem.Queryable().Where(o => o.Item.ID == i.ID).ToList();
            //foreach (var a in ass)
            //{
            //    repositoryCustumerOrderItem.Delete(a);
            //}

            //unit.SaveChanges();


            //var  as2 = repositorySupplierOffer.Queryable().Where(o => o.Item.ID == i.ID).ToList();
            //foreach (var a in as2)
            //{
            //    repositorySupplierOffer.Delete(a);
            //}

            //rep.Delete(i);


            //count = 0;



            //  SeedTemplateItems(db, unit);

            //IRepositorySupplierInfo repS = new RepositorySupplierInfo(db, unit);
            //IRepositoryUser repU = new RepositoryUser(db, unit);
            //IRepositoryItem repI = new RepositoryItem(db, unit);

            //User u = new User()
            //{
            //    Name = "Custumer 1",
            //    Email = "a@gmail.com",
            //    UserType = Model.Enums.UserType.Supplier,
            //    Password = "123"
            //};
            //repU.Insert(u);

            //IRepositoryRole repRole = new RepositoryRole(db, unit);
            //IServiceRole serviceRole = new ServiceRole(repRole);
            //string roleName = "Supplier";
            //serviceRole.AddUserOnRole(roleName, u);

            //SupplierInfo SupplierInto = new SupplierInfo()
            //{
            //    User = u
            //};
            //repS.Insert(SupplierInto);

            //Item item = repI.Queryable().FirstOrDefault();

            //SupplierOffer offer = new SupplierOffer()
            //{
            //    Item = item,
            //    Price = 5000.0,
            //    ProvidedDateDelivery = new DateTime(2017, 07, 26),
            //     SupplierInfo = SupplierInto
            //};

            //IRepositorySupplierOffer repOffer = new RepositorySupplierOffer(db, unit);
            // repOffer.Insert(offer);

            //var offer = repOffer.Queryable().FirstOrDefault();


            unit.SaveChanges();


        }



        private static void InitialSeed(ProcurementContext db, UnitOfWorkProcurement unit)
        {
            var roles = new List<Role>()
            {
                new Role() {  Name = "Admin" },
                new Role() {  Name = "Custumer" },
                new Role() {  Name = "Supplier" }
            };
            IRepositoryRole repRole = new RepositoryRole(db, unit);
            repRole.InsertRange(roles);
            unit.SaveChanges();

            User admin = new User()
            {
                Name = "Admin",
                UserType = Model.Enums.UserType.Admin,
                Password = "123",
                Email = "admin@gmail.com",
                Login = "Admin"
            };
            IRepositoryUser repUser = new RepositoryUser(db, unit);
            repUser.Insert(admin);
            AdminInfo adminInfo = new AdminInfo() {  User = admin };
            IRepositoryAdminInfo repAdminInfo = new RepositoryAdminInfo(db, unit);
            repAdminInfo.Insert(adminInfo);

            unit.SaveChanges();

            Role roleAdmin = repRole.Queryable().Where(r => r.Name == "Admin").FirstOrDefault();
            roleAdmin.Users.Add(admin);
            repRole.Update(roleAdmin);


            unit.SaveChanges();

        }


        private static void SeedTemplateItems(ProcurementContext db, UnitOfWorkProcurement unit)
        {


            //Half couplimg





            //Cross
            //45º lateral          
            //Flange

            //Para quase todos eles vale a seguinte lista de atributos:
            //            Code(UNSPSC)
            //Description
            //Reference standard(e.g.ASME B 16.4)
            //Diameter ou NPS(no caso das reduções são dois diâmetros)
            //Rating class (e.g. 150 lbs)
            //Material Specification
            //Grade
            //Endings
            //Thickness
            IRepositoryItemType repItemType = new RepositoryItemType(db, unit);
            IRepositoryTemplateItem repTemplateItem = new RepositoryTemplateItem(db, unit);
            IRepositoryTemplateAttribute repTemplateAttribute = new RepositoryTemplateAttribute(db, unit);
            IRepositoryItem repItem = new RepositoryItem(db, unit);
            IRepositoryAttributeValue repAttributeValue = new RepositoryAttributeValue(db, unit);


            SeedPipe(unit, repItem, repItemType, repTemplateItem, repTemplateAttribute, repAttributeValue);

            #region Elbow
            ItemType itemType = new ItemType() { Name = "Elbow" };
            repItemType.Insert(itemType);
            List<TemplateAttribute> attributes = new List<TemplateAttribute>()
            {               
                new TemplateAttribute() { Name="Reference standard", Order = 1},
                new TemplateAttribute() { Name="NPS", Order = 2},
                new TemplateAttribute() { Name="Rating class", Order = 3},
                new TemplateAttribute() { Name="Material Specification", Order = 4},
                new TemplateAttribute() { Name="Grade", Order = 5},
                new TemplateAttribute() { Name="Endings", Order = 6},
                new TemplateAttribute() { Name="Thickness", Order = 7},
            };
            repTemplateAttribute.InsertRange(attributes);
            TemplateItem templateItem = new TemplateItem() { Name = "Elbow 90°", ItemType = itemType, ModelAttributes = attributes };
            repTemplateItem.Insert(templateItem);

            attributes = new List<TemplateAttribute>()
            {
                new TemplateAttribute() { Name="Reference standard", Order = 1},
                new TemplateAttribute() { Name="NPS", Order = 2},
                new TemplateAttribute() { Name="Rating class", Order = 3},
                new TemplateAttribute() { Name="Material Specification", Order = 4},
                new TemplateAttribute() { Name="Grade", Order = 5},
                new TemplateAttribute() { Name="Endings", Order = 6},
                new TemplateAttribute() { Name="Thickness", Order = 7},
            };
            repTemplateAttribute.InsertRange(attributes);
            templateItem = new TemplateItem() { Name = "Elbow 90º Long Radius", ItemType = itemType, ModelAttributes = attributes };
            repTemplateItem.Insert(templateItem);


            attributes = new List<TemplateAttribute>()
            {
                new TemplateAttribute() { Name="Reference standard", Order = 1},
                new TemplateAttribute() { Name="NPS", Order = 2},
                new TemplateAttribute() { Name="Rating class", Order = 3},
                new TemplateAttribute() { Name="Material Specification", Order = 4},
                new TemplateAttribute() { Name="Grade", Order = 5},
                new TemplateAttribute() { Name="Endings", Order = 6},
                new TemplateAttribute() { Name="Thickness", Order = 7},
            };
            repTemplateAttribute.InsertRange(attributes);
            templateItem = new TemplateItem() { Name = "Elbow 45º", ItemType = itemType, ModelAttributes = attributes };
            repTemplateItem.Insert(templateItem);




            #endregion

            #region Tee
            itemType = new ItemType() { Name = "Tee" };
            repItemType.Insert(itemType);
            attributes = new List<TemplateAttribute>()
            {
              new TemplateAttribute() { Name="Reference standard", Order = 1},
                new TemplateAttribute() { Name="NPS", Order = 2},
                new TemplateAttribute() { Name="Rating class", Order = 3},
                new TemplateAttribute() { Name="Material Specification", Order = 4},
                new TemplateAttribute() { Name="Grade", Order = 5},
                new TemplateAttribute() { Name="Endings", Order = 6},
                new TemplateAttribute() { Name="Thickness", Order = 7},
            };
            repTemplateAttribute.InsertRange(attributes);
            templateItem = new TemplateItem() { Name = "Tee", ItemType = itemType, ModelAttributes = attributes };
            repTemplateItem.Insert(templateItem);
            #endregion

            #region Reducer
            itemType = new ItemType() { Name = "Reducer" };
            repItemType.Insert(itemType);
            attributes = new List<TemplateAttribute>()
            {
               new TemplateAttribute() { Name="Reference standard", Order = 1},
                new TemplateAttribute() { Name="NPS", Order = 2},
                new TemplateAttribute() { Name="Rating class", Order = 3},
                new TemplateAttribute() { Name="Material Specification", Order = 4},
                new TemplateAttribute() { Name="Grade", Order = 5},
                new TemplateAttribute() { Name="Endings", Order = 6},
                new TemplateAttribute() { Name="Thickness", Order = 7},
            };
            repTemplateAttribute.InsertRange(attributes);
            templateItem = new TemplateItem() { Name = "Reducer Concentric", ItemType = itemType, ModelAttributes = attributes };
            repTemplateItem.Insert(templateItem);


            attributes = new List<TemplateAttribute>()
            {
                new TemplateAttribute() { Name="Reference standard", Order = 1},
                new TemplateAttribute() { Name="NPS", Order = 2},
                new TemplateAttribute() { Name="Rating class", Order = 3},
                new TemplateAttribute() { Name="Material Specification", Order = 4},
                new TemplateAttribute() { Name="Grade", Order = 5},
                new TemplateAttribute() { Name="Endings", Order = 6},
                new TemplateAttribute() { Name="Thickness", Order = 7},
            };
            repTemplateAttribute.InsertRange(attributes);
            templateItem = new TemplateItem() { Name = "Reducer Eccentric", ItemType = itemType, ModelAttributes = attributes };
            repTemplateItem.Insert(templateItem);


            #endregion

            #region Flange
            itemType = new ItemType() { Name = "Flange" };
            repItemType.Insert(itemType);
            attributes = new List<TemplateAttribute>()
            {
                new TemplateAttribute() { Name="Reference standard", Order = 1},
                new TemplateAttribute() { Name="NPS", Order = 2},
                new TemplateAttribute() { Name="Rating class", Order = 3},
                new TemplateAttribute() { Name="Material Specification", Order = 4},
                new TemplateAttribute() { Name="Grade", Order = 5},
                new TemplateAttribute() { Name="Endings", Order = 6},
                new TemplateAttribute() { Name="Thickness", Order = 7},
            };
            repTemplateAttribute.InsertRange(attributes);
            templateItem = new TemplateItem() { Name = "Flange", ItemType = itemType, ModelAttributes = attributes };
            repTemplateItem.Insert(templateItem);
            #endregion

        }

        private static void SeedPipe(UnitOfWorkProcurement unit, IRepositoryItem repItem, IRepositoryItemType repItemType, IRepositoryTemplateItem repTemplateItem, IRepositoryTemplateAttribute repTemplateAttribute
            , IRepositoryAttributeValue repAttributeValue)
        {
            //Pipe_catalogue.csv

            #region Pipe
            ItemType itemType = new ItemType() { Name = "Pipe" };
            repItemType.Insert(itemType);

     
            TemplateAttribute spec = new TemplateAttribute() { Name = "Specification", Order = 1 };
            TemplateAttribute grade = new TemplateAttribute() { Name = "Grade", Order = 2 };
            TemplateAttribute nps = new TemplateAttribute() { Name = "NPS", Order = 3 };
            TemplateAttribute thickness = new TemplateAttribute() { Name = "Thickness", Order = 54};
            TemplateAttribute welded = new TemplateAttribute() { Name = "Welded/Seamless", Order = 5 };
            TemplateAttribute kg = new TemplateAttribute() { Name = "Kg/m", Order = 6 };

            //UNSPSC Code;Specification;Grade;NPS;Thickness;Welded/Seamless;Kg/m
            List<TemplateAttribute> attributes = new List<TemplateAttribute>()
            {                
             spec,
             grade ,
             nps ,
             thickness ,
             welded,
             kg,        
                new TemplateAttribute() { Name="Reference standard", Order =7},
                new TemplateAttribute() { Name="Rating class", Order =8},
                new TemplateAttribute() { Name="Material Specification", Order =9 },
                new TemplateAttribute() { Name="Endings", Order =10},

            };
            repTemplateAttribute.InsertRange(attributes);
            TemplateItem pipe = new TemplateItem() { Name = "Pipe", ItemType = itemType, ModelAttributes = attributes };
            repTemplateItem.Insert(pipe);

            IEnumerable<string> lines = System.IO.File.ReadLines(@"C:\Proc\Pipe_catalogue.csv");

            int count = 1;

            foreach (var line in lines)
            {
                string[] columnsValues = null;
                if (count == 1)
                {

                    columnsValues = line.Split(';');
                }
                else
                {
                    columnsValues = line.Split(';');

                    Item item = new Item()
                    {
                        Template = pipe,
                        UNSPSC = columnsValues[0]
                    };
                    repItem.Insert(item);
                    unit.SaveChanges();

                    List<AttributeValue> listAttrValues = new List<AttributeValue>() {                       
                            new AttributeValue() { Item = item, TemplateAttribute = spec, Value = columnsValues[1] },
                            new AttributeValue() { Item = item, TemplateAttribute = grade, Value = columnsValues[2] },
                            new AttributeValue() { Item = item, TemplateAttribute = nps, Value = columnsValues[3] },
                            new AttributeValue() { Item = item, TemplateAttribute = thickness, Value = columnsValues[4] },
                            new AttributeValue() { Item = item, TemplateAttribute = welded, Value = columnsValues[5] },
                            new AttributeValue() { Item = item, TemplateAttribute = kg, Value = columnsValues[6] }
                };
                    repAttributeValue.InsertRange(listAttrValues);
                    item.AttributeValues = listAttrValues;
                    repItem.Update(item);
                }
                count++;

                if (count >= 700)
                {
                    break;
                }
            }




            #endregion
        }
    }
}
