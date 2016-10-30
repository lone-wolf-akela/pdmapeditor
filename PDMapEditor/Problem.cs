using System.Collections.Generic;

namespace PDMapEditor
{
    public class Problem
    {
        public static List<Problem> Problems = new List<Problem>();

        public ProblemTypes Type;
        public string Description;

        public Problem(ProblemTypes type, string description)
        {
            if (IsProblemExisting(description))
            {
                return;
            }

            Type = type;
            Description = description;

            Log.WriteLine(Type.ToString() + ": " + description);
            Problems.Add(this);
            Program.main.AddProblem(this);
        }

        private bool IsProblemExisting(string description)
        {
            foreach(Problem problem in Problems)
            {
                if (problem.Description == description)
                    return true;
            }

            return false;
        }
    }

    public enum ProblemTypes
    {
        ERROR = 1,
        WARNING = 2,
    }
}
