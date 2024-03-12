using System.Text.RegularExpressions;

namespace FinalProjectRevised
//Author: Demetrius Richards
//Purpose: Final Project
//Course: COMP-003A-L01
{
    namespace FinalProjectRevised

    {
        class PatientIntakeForm
        {

            static void Main(string[] args)
            {

                Console.WriteLine("Welcome to our New Patient Intake Form\nPlease fill out the upcoming instructions");
                try //the try code does have a purpose and it is to handle exceptions that might happen during running the code, there is a catch not too far down so any exceptions are caught by the block, this lets the program be able to run gracefully by displaying an error on the screen instead of just downright shutting it down.
                    //if the try block is removed and an exception happens it will not be caught which could lead to a crash or other behaviors we dont want
                {

                    //Here we will collect the users input for thier First name and all the other things
                    //the first parameter is the prompt message that will be displayed to the user
                    //the second parameter (InputStringWithValidation) is a regular expression that defines the valid input format. For names we are limiting the input to alphabets and symbols
                    //the third parameter here (InputIntWithValidation) these are the minimum and maximim valid values for integer input 1900 - Present
                    string firstName = InputStringWithValidation("Enter your First Name (Alphabets only): ", @"^[a-zA-Z]+$");
                    string lastName = InputStringWithValidation("Enter your Last Name (Alphabets only): ", @"^[a-zA-Z]+$");
                    int birthYear = InputIntWithValidation("Enter your Birth Year (1900-2024): ", 1900, DateTime.Now.Year);
                    string gender = InputStringWithValidation("Enter your Gender (M/F/O); ", @"^[MFOmfo]$", true);


                    //here will be the questionnaire

                    List<string> questions = GenerateQuestions();
                    List<string> responses = new List<string>();


                    //Collect the responses with loops

                    Console.WriteLine("\nPlease answer the upcoming questions:");
                    //the purpose for var ised for type inference letting the compiler to infer the type of variable question based on what is assigned to it, here specifically the case would be the type of elements that are in the questions collection
                    //purpose of variable questions here is to hold the current element in the questions collection as the foreach loop iterates over it
                    foreach (var question in questions)
                    {
                        string response = CollectResponse(question);
                        responses.Add(response);
                    }

                    //here we calculate age and convert gender to full desctription

                    int age = CalculateAge(birthYear);
                    string fullGender = ConvertGender(gender);
                    //-----------------------------------------------------------------------------------------------------------------------

                    //here we display profile summary and questionnaire responses

                    DisplayProfileSummary(firstName, lastName, age, fullGender);
                    DisplayResponses(questions, responses);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

            }
            /// <summary>
            /// here are the input methods with validation
            /// </summary>
            /// <param name="prompt">message displayed to user to prompt input</param>
            /// <param name="pattern"> the regular expression pattern the input is validated against</param>
            /// <param name="toUpperCase">if true, it converts the users input to upper case before returning</param>
            /// <returns>the validated string input by the user. the string is converted to upper case if toUpperCase is true</returns>
            /// the purpose of the default value (false) for the toUpperCase parameter is to give an optional behavior for this method. this lets the method be more flexible
            private static string InputStringWithValidation(string prompt, string pattern, bool toUpperCase = false)
            {
                while (true) //a return statement will make it stop. this one stops when valid input is given that is not empty
                {
                    Console.Write(prompt);
                    string input = Console.ReadLine();
                    //the ! is used to check that input is not empty
                    //the purpose of regex is to check if the inputs that were given match the regulat expression pattern 
                    if (!string.IsNullOrEmpty(input) && Regex.IsMatch(input, pattern))
                    {
                        return toUpperCase ? input.ToUpper() : input; //purpose of ? is to check if toUpperCase is true. if it is it returns input.ToUpper()
                    }
                    Console.WriteLine("Invalid input. Try again");

                }
            }
            /// <summary>
            /// prompts the user for the integer input and validates it if it is within the specified range
            /// </summary>
            /// <param name="prompt">the text prompt to display to the user</param>
            /// <param name="min"> the minimum acceptable value for input</param>
            /// <param name="max">the maximimum acceptable value for input</param>
            /// <returns> the validated integer that alls within specified range</returns>
            private static int InputIntWithValidation(string prompt, int min, int max)
            {
                while (true) //this loop stops when the return statement is executed, this happens when a valid integer input is entered by the user and it is within specified range
                {
                    Console.Write(prompt);
                    //the purpose of the parse is so it can check if the input can be made into an integer.
                    //the out keyword is used for the second parameter of the parse which lets the method return the parsed integer value back to its caller
                    if (int.TryParse(Console.ReadLine(), out int input) && input >= min && input <= max)
                    {
                        return input;
                    }
                    Console.WriteLine($"Invalid input. Enter a value between {min} and {max}");


                }
            }

            /// <summary>
            /// here we have something that will  make questions for our questionnaire
            /// </summary>
            /// <returns> List of strings containing different questions that are health related to be used in questionnaire</returns>
            private static List<string> GenerateQuestions()
            {
                return new List<string>
            {
                "Are you experiencing any symptoms as of right now?",
                "Have you had any surgeries in the past year?",
                "Have you been diagnosed with any chronic illnesses?",
                "Are you currently taking any type of medication?",
                "Do you have any type of allergies?",
                "Have you been hospitalized in the past year?",
                "Do you drink alcohol?",
                "Do you smoke?",
                "How often do you excercise?",
                "Does your family have a history for any major illnesses?",
            };
            }

            /// <summary>
            /// here we will collect a response with validation to make sure it is not empty
            /// </summary>
            /// <param name="question">the question is meant to be shown to the user</param>
            /// <returns> a non-empty string which is going to be the users response to the question</returns>
            private static string CollectResponse(string question)
            {
                Console.WriteLine(question); string response; do
                {
                    response = Console.ReadLine();
                    if (string.IsNullOrEmpty(response))
                    {
                        Console.WriteLine("Response cannot be null or empty. Please retry your response.");
                    }
                } while (string.IsNullOrEmpty(response));
                return response;
            }

            /// <summary>
            /// calculating age based on birth year provided
            /// </summary>
            /// <param name="birthYear">the year of birth the user put in to calculate age from</param>
            /// <returns> the calculated age as an integer</returns>
            private static int CalculateAge(int birthYear)
            {
                return DateTime.Now.Year - birthYear;
            }

            /// <summary>
            /// converting gender input to the full description
            /// </summary>
            /// <param name="gender">a one letter code representing the gender (M, F, O) male, female, or other</param>
            /// <returns> a string representing the full description of the gender</returns>
            private static string ConvertGender(string gender)
            {
                switch (gender.ToUpper())
                {
                    case "M": return "Male";
                    case "F": return "Female";
                    case "O": return "Other not listed";
                    default: return "Unknown"; //even if it is unreachable it is here just incase to ensure the method can handle any unexpected inputs just fine, you never know
                }
            }

            /// <summary>
            /// Display the users profile summary
            /// </summary>
            /// <param name="firstName">first name of user</param>
            /// <param name="lastName">last name of user</param>
            /// <param name="age">age of user</param>
            /// <param name="gender">gender of user</param>
            private static void DisplayProfileSummary(string firstName, string lastName, int age, string gender)
            {
                Console.WriteLine("\nProfile Summary:");
                Console.WriteLine($"Full Name: {firstName} {lastName}");
                Console.WriteLine($"Age: {age}");
                Console.WriteLine($"Gender: {gender}");


            }
            /// <summary>
            /// this will display the questions and responses
            /// </summary>
            /// <param name="questions">list of strings containing our questions</param>
            /// <param name="responses"> a list of strings containing the responses that correspond to the questions</param>
            private static void DisplayResponses(List<string> questions, List<string> responses)
            {
                Console.WriteLine("\nQuestionnaire Responses:");
                //questions.Count is here because we use it to get the number of elements in the list questions. the for loop is using this to see how much times to iterate

                //i is at zero to make sure that the loop starts at the first element of the questions list
                for (int i = 0; i < questions.Count; i++)
                {
                    //its i + 1 and its here because we want it to display the questions and responses number in a readable format
                    Console.WriteLine($"{i + 1}. {questions[i]}");
                    Console.WriteLine($"Response: {responses[i]}\n");

                }
            }



        }
    }

}
