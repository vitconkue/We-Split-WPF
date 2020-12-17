using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace We_Split_WPF.Model
{
    public class TripModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsFinished { get; set; }

        public List<MemberInTripModel> memberList {get;set; }

        public List<ExpenseModel> expensesList { get; set; }

        public List<PlaceModel> placeList { get; set; }
        public void ShowConsole()
        {
            Console.WriteLine("Basic information: "); 
            
            Console.WriteLine($"ID : {ID}, Name: {Name}, IsFinished: {IsFinished}");

            Console.WriteLine("List information: ");

            foreach(var member in this.memberList)
            {
                member.ShowConsole(); 
            }

            foreach(var expense in this.expensesList)
            {
                expense.ShowConsole(); 
            }
            Console.WriteLine("Money Issue: ");
            Console.WriteLine($"Total expenses:{SumExpenses} ");
            Console.WriteLine($"Money received from member: {MoneyReceived}");
        }

        public int SumExpenses
        {
            get
            {
                int needed = 0;
                foreach (ExpenseModel ex in expensesList)
                {
                    needed += ex.AmountMoney;
                }
                return needed; 
            }
        }

        public int MoneyReceived
        {
            get
            {
                int received = 0; 
                foreach(MemberInTripModel member in memberList)
                {
                    received += member.MoneyPaid; 
                }
                return received;
            }
        }

        public void AddAlreadyExistedMember(MemberModel memberModel)
        {
            var newMemberJoin = new MemberInTripModel
            {
                ID = memberModel.ID,
                Name = memberModel.Name,
               
            };

            this.memberList.Add(newMemberJoin);
            // add in database 
            DatabaseAccess.AddExistedToJoinTrip(newMemberJoin, this.ID); 
            
        }
        /// <summary>
        /// ////////// CHUA XU LI ID
        /// </summary>
        /// <param name="memberModel"></param>
        public void AddNewMemberToTrip(MemberModel memberModel) // pass model with Name only
        {
            // add new member to member table
            int newID = DatabaseAccess.AddMemberToDB(memberModel);
            // Now she exists, Make her join the trip
            memberModel.ID = newID; 
            AddAlreadyExistedMember(memberModel); 

        }

        public void AddExpense(ExpenseModel expense) // pass model with name and amount only
        {
            int newNum = DatabaseAccess.AddNewExpenseToTrip(expense, this.ID);

            ExpenseModel toAddToTripModel = new ExpenseModel
            {
                TripID = this.ID,
                Number = newNum,
                AmountMoney = expense.AmountMoney,
                Name = expense.Name
            };
            this.expensesList.Add(toAddToTripModel); 

            Console.WriteLine($"Added expense num: {newNum}");
        }
        
        public void AddPlace(PlaceModel place)  // pass model with name,information,datestart,datefinish
        {
            int newNum = DatabaseAccess.AddNewPlaceToTrip(place, this.ID);
            PlaceModel toAddToTripModel = new PlaceModel
            {
                TripID = this.ID,
                Number = newNum,
                Name = place.Name,
                Information = place.Information,
                DateStart = place.DateStart,
                DateFinish = place.DateFinish
            };
            this.placeList.Add(toAddToTripModel);

            Console.WriteLine($"Added place num: {newNum}"); 
        }
        public void GetMoreMoneyFromMember(MemberInTripModel memberInTrip, int amount)
        {
            memberInTrip.MoneyPaid += amount;

            // update database
            bool result = DatabaseAccess.UpdateMemberAddMoneyPaid(this.ID, memberInTrip, amount); 

            if(!result)
            {
                Console.WriteLine("Fail add more money");
            }
        }

        public void ToogleIsFinished()
        {
            IsFinished = !IsFinished;
            // update database
            DatabaseAccess.setIsFinishedTrip(IsFinished, this.ID); 
        }
        public int CalculateMoneyLack()
        {
            return SumExpenses - MoneyReceived; 
        }
        public Uri ImageLink
        {
            get
            {
                string file = AppDomain.CurrentDomain.BaseDirectory;
                Uri path = new Uri($"{file}Data\\Images\\TripsImage\\{ID}\\Main\\main.png");
                return path;
            }
        }
        public List<Uri> PlaceImages
        {
            get
            {
                List<Uri> Temp = new List<Uri>();
                string file = AppDomain.CurrentDomain.BaseDirectory;
                string path = $"{file}Data\\Images\\TripsImage\\{ID}\\Location\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                DirectoryInfo Folder = new DirectoryInfo(path);
                
                var Images = Folder.GetFiles();
                foreach(var image in Images)
                {
                    Uri direct = new Uri(image.FullName);
                    Temp.Add(direct);
                }
                return Temp;
            }
        }


    }
}
