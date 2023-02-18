using LinqSippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace LinqSnippets
{

    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "Volkswagen Golf",
                "Volkswagen California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"

            };

            // 1 - SELECT * of cars (SELECT ALL CARS)
            var carList = from car in cars select car;

            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            // 2 - SELECT WHERE car is Audi
            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }

        }

        // Number Examples
        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Each number multiplied by 3; take all numbers but 9
            // Order numbers by ascending value 

            var processedNumberList =
               numbers
                .Select(num => num * 3)
                .Where(num => num != 9)
                .OrderBy(num => num); // order ascending
        }

        static public void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };

            // 1 - Search for the first element
            var first = textList.First();

            // 2 - First element that is "c"
            var cText = textList.First(text => text.Equals("c"));

            // 3 - First element that contains "j"
            var jText = textList.First(text => text.Contains("j"));

            // 4 - First element that contains Z if not dafault
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z"));

            // 5 - Last element that contains Z if not dafault
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z"));

            // 6 - Single values
            var uniqueTexts = textList.Single();
            var uniqueOrDefaultTexts = textList.SingleOrDefault();

            // Comparision between lists
            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            // Obtain {4 , 8}
            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers); // only { 4, 8 )
        }

        static public void MultipleSelects()
        {
            // SELECT MANY
            string[] myOpinions =
            {
                "Opinion 1 text 1",
                "Opinion 2 text 2",
                "Opinion 3 text 3",
            };

            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id =1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Rui",
                            Email = "rlago@sapo.pt",
                            Salary = 3500
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Carlos",
                            Email = "carlo@sapo.pt",
                            Salary = 3000
                        },
                        new Employee
                        {
                               Id = 3,
                            Name = "Popi",
                            Email = "popi@gmail.com",
                            Salary = 2800
                        }
                    }
                },
                 new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 4,
                            Name = "Ana",
                            Email = "ana.lopes@gmail.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 5,
                            Name = "Afonso",
                            Email = "af@sapo.pt",
                            Salary = 2400
                        },
                        new Employee
                        {
                               Id = 6,
                            Name = "Alex",
                            Email = "alexander.n@gmail.com",
                            Salary = 3800
                        }
                    }
                }
            };

            // Obtain all enployees of all enterprises
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Know if any List is empty empty
            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // At least all enterprises has employees with a 1000Euro salary
            bool hasEmployeeWithSalaryMoreThanOrEqual1000 =
                enterprises.Any(enterprise =>
                    enterprise.Employees.Any(employee =>
                        employee.Salary >= 1000));

        }

        static public void linqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            // INNER JOIN
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                    secondList,
                    element => element,
                    secondElement => secondElement,
                    (element, seconElement) => new { element, seconElement }
                );

            // OUTER JOIN - LEFT
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, secondElement = secondElement };


            //  OUTER JOIN - RIGHT
            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            // UNION
            var unionList = leftOuterJoin.Union(rightOuterJoin);

        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };

            // SKIP Functions
            var skipTwoFirstValues = myList.Skip(2);

            var skipLastTwoValues = myList.SkipLast(2);

            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4);

            // TAKE Functions
            var takeFirstTwoValues = myList.Take(2);

            var takeLastTwoValues = myList.TakeLast(2);

            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4);

        }

        // Paging with Skip and Take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumebr, int resultsPerPage)
        {
            int startIndex = (pageNumebr - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }


        // Variables
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquare = Math.Pow(number, 2)
                               where nSquare > average
                               select number;

            Console.WriteLine("Average: {0}", numbers.Average());

            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Query: number: {0} square: {1}", number, Math.Pow(number, 2));
            }
        }
        // ZIP
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringsNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringsNumbers, (number, word) => number + "=" + word);
            // We are going to obtain a list like {"1=one", 2="two"...} and so on
        }
        // Repeat
        static public void repeatRangeLinq()
        {
            // Generate a collection of values from 1 - 1000 -> Range
            IEnumerable<int> fisrt1000 = Enumerable.Range(1, 1000);

            //var aboveAverage = from number in first1000
            //                   select number;

            // Repeat a value N times
            IEnumerable<string> fiveXs = Enumerable.Repeat("x", 5); // A list of xxxxx -> five x`s



        }
        static public void studentsLinq()
        {
            // List of students
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Rui",
                    Grade = 60,
                    Certified = true,
                },
                 new Student
                {
                    Id = 2,
                    Name = "Marta",
                    Grade = 45,
                    Certified = false,
                },
                  new Student
                {
                    Id = 3,
                    Name = "Marco",
                    Grade = 15,
                    Certified = false,
                },
                   new Student
                {
                    Id = 4,
                    Name = "Teresa",
                    Grade = 70,
                    Certified = true,
                },
                    new Student
                {
                    Id = 5,
                    Name = "Carol",
                    Grade = 80,
                    Certified = true,
                }


            };

            // Obtaining the certified students
            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertifiedStudents = from student in classRoom
                                       where student.Certified == false
                                       select student;

            var approvedStudentsNames = from student in classRoom
                                        where student.Grade >= 50 && student.Certified == true
                                        select student.Name;
        }
        // All
        static public void AllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };

            bool allAreSmallerThan10 = numbers.All(x => x < 10); // true
            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2); // false

            var emptyList = new List<int>();

            bool allNumbersAreGreaterThan0 = numbers.All(x => x >= 0); // true
        }
        // Aggregate
        static public void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Sum of all numbers
            int sum = numbers.Aggregate((prevsum, current) => prevsum + current);

            // 0, 1 => 1
            // 1, 2 => 3
            // 3, 4 => 7 etc.

            string[] words = { "Hello", "my", "name", "is", "Rui" };
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);

            // "", "hello"
            // "", "hello", "my" etc -> it`s an iteration

        }
        // Distinct
        public static void distinctValue()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };

            IEnumerable<int> distinctValues = numbers.Distinct();

        }
        // GroupBy
        static public void groupByExamples()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Obtain only even numbers generating two groups
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            foreach (var group in grouped)
            {
                foreach (var value in group)
                {
                    Console.WriteLine(value); // First the odd and then the even
                }

            }

            // Another example
            var classRoom = new[]
           {
                new Student
                {
                    Id = 1,
                    Name = "Rui",
                    Grade = 60,
                    Certified = true,
                },
                 new Student
                {
                    Id = 2,
                    Name = "Marta",
                    Grade = 45,
                    Certified = false,
                },
                  new Student
                {
                    Id = 3,
                    Name = "Marco",
                    Grade = 15,
                    Certified = false,
                },
                   new Student
                {
                    Id = 4,
                    Name = "teresa",
                    Grade = 70,
                    Certified = true,
                },
                    new Student
                {
                    Id = 5,
                    Name = "Carol",
                    Grade = 80,
                    Certified = true,
                }


            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified && student.Grade >= 50);

            // Obtaining two groups, first the no certified and 2. the certified ones

            foreach (var group in certifiedQuery)
            {
                Console.WriteLine("---------- {0} --------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name); // 1st The certified an the the no certified
                }
            }
        }

        static public void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id =1,
                    Title = "First post",
                    Content = "First content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "First comment",
                            Content = "Content"
                        },
                         new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "Second comment",
                            Content = "Content"
                        },
                          new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "Third comment",
                            Content = "More Content"
                        }
                    }
                },
                new Post()
                {
                    Id =2,
                    Title = "Second post",
                    Content = "Second content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "Other comment",
                            Content = "New Content"
                        },
                         new Comment()
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title = "New other comment",
                            Content = "Other Content"
                        },

                    }
                }
            };

            var commentsWithContent = posts.SelectMany(
                post => post.Comments,
                (post, Comment) => new {
                    postId = post.Id,
                    CommentContent = Comment.Content
                });

        }
    }
}