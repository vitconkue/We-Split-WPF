﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using We_Split_WPF.Model;
using Dapper; 

namespace We_Split_WPF.Model
{
    class DatabaseAccess
    {
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        // Load 1 trip
        public static TripModel LoadSingleTrip(int IDToFind)
        {
            TripModel result = new TripModel();
            string sqlString = $"SELECT * FROM TRIP WHERE ID = {IDToFind}";
            using (var cnn = new SQLiteConnection(LoadConnectionString()))
            {
                TripModel output = cnn.QueryFirst<TripModel>(sqlString, new DynamicParameters());
                result = output;


                string getAllMemberSql = $"SELECT MEMBER.*, MONEYPAID " +
                        $"FROM MEMBER JOIN MEMBERJOINTRIP ON MEMBER.ID = MEMBERJOINTRIP.MEMBERID" +
                        $" WHERE TRIPID = {IDToFind} ";
                List<MemberInTripModel> outputMemberList = cnn.Query<MemberInTripModel>(getAllMemberSql, new DynamicParameters()).ToList();
                result.memberList = outputMemberList;


                string getAllExpenseSql = $"SELECT * FROM EXPENSE WHERE TRIPID = {IDToFind} ";
                List<ExpenseModel> outputExpenseList = cnn.Query<ExpenseModel>(getAllExpenseSql, new DynamicParameters()).ToList();
                result.expensesList = outputExpenseList;

                string getAllPlaceSql = $"SELECT * FROM PLACE WHERE TRIPID = {IDToFind}";
                List<PlaceModel> outputPlaceList = cnn.Query<PlaceModel>(getAllPlaceSql, new DynamicParameters()).ToList();
                result.placeList = outputPlaceList;
            }
            return result;

        }

        // Loads all the trip
        public static List<TripModel> LoadAllTrips()
        {
            List<TripModel> result = new List<TripModel>();
            string sqlString = "SELECT * FROM TRIP";
            using (var cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TripModel>(sqlString, new DynamicParameters());
                result = output.ToList();

                foreach (TripModel trip in result)
                {
                    int IDToFind = trip.ID;

                    string getAllMemberSql = $"SELECT MEMBER.*, MONEYPAID " +
                        $"FROM MEMBER JOIN MEMBERJOINTRIP ON MEMBER.ID = MEMBERJOINTRIP.MEMBERID" +
                        $" WHERE TRIPID = {IDToFind} ";
                    List<MemberInTripModel> outputMemberList = cnn.Query<MemberInTripModel>(getAllMemberSql, new DynamicParameters()).ToList();
                    trip.memberList = outputMemberList;

                    string getAllExpenseSql = $"SELECT * FROM EXPENSE WHERE TRIPID = {IDToFind} ";
                    List<ExpenseModel> outputExpenseList = cnn.Query<ExpenseModel>(getAllExpenseSql, new DynamicParameters()).ToList();
                    trip.expensesList = outputExpenseList;

                    string getAllPlaceSql = $"SELECT * FROM PLACE WHERE TRIPID = {IDToFind}";
                    List<PlaceModel> outputPlaceList = cnn.Query<PlaceModel>(getAllPlaceSql, new DynamicParameters()).ToList();
                    trip.placeList = outputPlaceList;

                }
            }
            return result;
        }

        // Set IsFinished Trip
        public static bool setIsFinishedTrip(bool toSet, int tripID)
        {
            int x = toSet ? 1 : 0;
            string sqlString = $"UPDATE TRIP " +
                $"SET ISFINISHED = {x} " +
                $"WHERE ID = {tripID}";
            using (var cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(sqlString);
            }
            return toSet;
        }


        // Load all member in database
        public static List<MemberModel> LoadAllMember()
        {
            List<MemberModel> result = new List<MemberModel>();
            string sqlString = "SELECT * FROM MEMBER";

            using (var cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var outputQuery = cnn.Query<MemberModel>(sqlString, new DynamicParameters());
                result = outputQuery.ToList();
            }

            return result;
        }

        // Save new member to database 
        public static int AddMemberToDB(MemberModel member)
        {
            string sqlString = "INSERT INTO MEMBER(Name) VALUES (@Name)";
            int newID = 0;

            using (var cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // execute insert
                cnn.Execute(sqlString, member);
                // get new ID
                newID = cnn.QueryFirst<int>("SELECT IFNULL(MAX(ID),0) FROM MEMBER", new DynamicParameters());
            }

            return newID;
        }

        // Save existed member to MemberJoinTrip table

        public static bool AddExistedToJoinTrip(MemberInTripModel memberInTrip, int tripID)
        {
            int result = 0;

            string sqlString = $"INSERT INTO MEMBERJOINTRIP VALUES({tripID},@ID,@MoneyPaid)";
            using (var cnn = new SQLiteConnection(LoadConnectionString()))
            {
                result = cnn.Execute(sqlString, memberInTrip);
            }
            return (result == 0) ? false : true;

        }

        // Add new expense to trip 
        public static int AddNewExpenseToTrip(ExpenseModel expense, int tripID)
        {
            int currentExpenseNumber = 0;
            using (var cnn = new SQLiteConnection(LoadConnectionString()))
            {
                currentExpenseNumber = cnn.QueryFirst<int>($"SELECT IFNULL(MAX(NUMBER),0) FROM EXPENSE WHERE TRIPID = {tripID} ");

                cnn.Execute($"INSERT INTO EXPENSE VALUES ({tripID}, {currentExpenseNumber + 1}, @Name,@AmountMoney)", expense);

            }
            return currentExpenseNumber + 1;
        }

        public static bool UpdateMemberAddMoneyPaid(int tripID, MemberInTripModel memberInTrip, int amount)
        {
            string sqlString = $"UPDATE MEMBERJOINTRIP " +
                $"SET MONEYPAID = MONEYPAID + {amount} " +
                $" WHERE TRIPID = {tripID} AND MEMBERID = @ID";
            int result = 0;
            using (var cnn = new SQLiteConnection(LoadConnectionString()))
            {
                result = cnn.Execute(sqlString, memberInTrip);
            }
            return result == 0 ? false : true;
        }

        // Add new place to trip
        public static int AddNewPlaceToTrip(PlaceModel place, int tripID)
        {
            int currrentPlaceNumber = 0;
            using (var cnn = new SQLiteConnection(LoadConnectionString()))
            {
                currrentPlaceNumber = cnn.QueryFirst<int>($"SELECT IFNULL(MAX(NUMBER),0) FROM PLACE WHERE TRIPID = {tripID}");
                cnn.Execute($"INSERT INTO PLACE VALUES ({tripID},{currrentPlaceNumber + 1}, @Name, @Information,@DateStart,@DateFinish)", place);
            }
            return currrentPlaceNumber + 1; 
        }

    }
}