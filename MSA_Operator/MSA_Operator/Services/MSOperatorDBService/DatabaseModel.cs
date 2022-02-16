using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace MSOperatorDBService
{
    public class DatabaseModel
    {
      
        /// <summary>
        /// Add operator to db table
        /// </summary>
        /// <param name="o"></param>
        public void AddOperator(Operators o)
        {
            using (var contex = new MSAOperatorDBEntities())
            {
                contex.Operators.Add(o);
                contex.SaveChanges();
            }
        }
        /// <summary>
        /// Removes operator from table by given ID
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteOperatorByID(int ID)
        {
            using (var contex = new MSAOperatorDBEntities())
            {
                try
                {
                    Operators operatorToDelete = contex.Operators.Where(b => b.OperatorID == ID).FirstOrDefault();
                    if (operatorToDelete != null)
                    {
                        contex.Operators.Remove(operatorToDelete);
                        contex.SaveChanges();
                    }
                }catch(Exception e)
                {

                }
            }
        }
        /// <summary>
        /// Changes operator password 
        /// </summary>
        public void ChangeOperatorPassword(int ID, string password)
        {
            using (var contex = new MSAOperatorDBEntities())
            {
                Operators operatorToPasswordChange = contex.Operators.Where(b => b.OperatorID == ID).FirstOrDefault();
                if (operatorToPasswordChange != null)
                {
                    try
                    {
                        contex.Operators.Remove(operatorToPasswordChange);
                        operatorToPasswordChange.Password = password;
                        contex.Operators.Add(operatorToPasswordChange);                    
                        contex.SaveChanges();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// Validation for operator login credetials
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateOperatorCredentials(string username, string password)
        {
            bool isValid = false;

            using (var contex = new MSAOperatorDBEntities())
            {
                try
                {
                    Operators operatorToPasswordChange = contex.Operators.Where(b => b.Username == username && b.Password == password).FirstOrDefault();
                    if (operatorToPasswordChange != null)
                        isValid = true;
                    }
                catch (Exception e)
                {

                }
            }
            return isValid;
        }


        /// <summary>
        /// Get list of all robots from database
        /// </summary>
        /// <returns></returns>
        public List<Robots> GetAllRobots()
        {
            List<Robots> robots = new List<Robots>();
            using (var contex = new MSAOperatorDBEntities())
            {
                try
                {
                    robots = contex.Robots.ToList();
                }
                catch (Exception e)
                {

                }
            }
            return robots;
        }

        /// <summary>
        /// Add robot to database
        /// </summary>
        /// <param name="r"></param>
        public void AddRobotToDB(Robots r)
        {
            using (var contex = new MSAOperatorDBEntities())
            {
                contex.Robots.Add(r);
                contex.SaveChanges();
            }
        }
    }
}
