﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLineParser.Tests.Helpers;
using CommandLineParser.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandLineParser.Tests
{
    [TestClass]
    public class ShortOptionTests
    {
        [TestMethod, Description("program -a")]
        public void Short_Option_Mapped_To_Boolean_With_NO_Value_Defaults_To_True()
        {
            string[] args = CommandManager.CommandLineToArgs("program -a");
            CommandParser parser = new CommandParser(args);
            var model = parser.Parse<TestModel>();
            Assert.IsTrue(model.IsAdmin); 
        }

        [TestMethod, Description("program -u=mykeels")]
        public void Short_Option_Can_Be_Joined_With_Value_Via_Equals_Sign()
        {
            string[] args = CommandManager.CommandLineToArgs("program -u=mykeels");
            CommandParser parser = new CommandParser(args);
            var model = parser.Parse<TestModel>();
            Assert.AreEqual(model.Username, "mykeels");
        }

        [TestMethod, Description("program -asu=mykeels")]
        public void Short_Option_Can_Be_Aggregated_And_Joined_With_Value_Via_Equals_Sign()
        {
            string[] args = CommandManager.CommandLineToArgs("program -asu=mykeels");
            CommandParser parser = new CommandParser(args);
            var model = parser.Parse<TestModel>();
            Assert.IsTrue(model.IsAdmin);
            Assert.IsTrue(model.IsSuperAdmin);
            Assert.AreEqual(model.Username, "mykeels");
        }

        [TestMethod, Description("program -u mykeels")]
        public void Short_Option_Can_Be_Followed_By_Space_And_Value()
        {
            string[] args = CommandManager.CommandLineToArgs("program -u mykeels");
            CommandParser parser = new CommandParser(args);
            var model = parser.Parse<TestModel>();
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(model));
            Assert.AreEqual(model.Username, "mykeels");
        }

        [TestMethod, Description("program -umykeels")]
        public void Short_Option_Can_Be_Followed_By_Value_Immediately()
        {
            string[] args = CommandManager.CommandLineToArgs("program -umykeels");
            CommandParser parser = new CommandParser(args);
            var model = parser.Parse<TestModel>();
            Assert.AreEqual(model.Username, "mykeels");
        }

        [TestMethod, Description("program -sumykeels")]
        public void Short_Option_Can_Be_Aggregated_And_Followed_By_Value_Immediately()
        {
            string[] args = CommandManager.CommandLineToArgs("program -saumykeels");
            CommandParser parser = new CommandParser(args);
            var model = parser.Parse<TestModel>();
            Assert.IsTrue(model.IsSuperAdmin);
            Assert.AreEqual(model.Username, "mykeels");
        }

        [TestMethod, Description("program -sa")]
        public void Short_Option_With_NO_Values_Can_Be_Aggregated()
        {
            string[] args = CommandManager.CommandLineToArgs("program -sa");
            CommandParser parser = new CommandParser(args);
            var model = parser.Parse<TestModel>();
            Assert.IsTrue(model.IsAdmin);
            Assert.IsTrue(model.IsSuperAdmin);
        }
    }
}
