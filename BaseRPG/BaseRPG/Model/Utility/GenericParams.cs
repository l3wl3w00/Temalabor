//using BaseRPG.Model.Interfaces.Skill;
//using System.Collections.Generic;

//namespace BaseRPG.Model.Utility
//{
//    public class GenericParams
//    {

//        private Dictionary<string, object> parameters = new();
//        private Dictionary<string, double> numberParameters = new();

//        public GenericParams(Dictionary<string, object> parameters)
//        {
//            this.parameters = parameters;
//        }

//        public GenericParams(Dictionary<string, double> numberParameters)
//        {
//            this.numberParameters = numberParameters;
//        }

//        public GenericParams(Dictionary<string, object> parameters, Dictionary<string, double> numberParameters) : this(parameters)
//        {
//            this.numberParameters = numberParameters;
//        }

//        public GenericParams()
//        {
//        }
//        public void AddParam(string name, object param)
//        {
//            parameters.Add(name, param);
//        }
//        public void AddParamDouble(string name, double param)
//        {
//            numberParameters.Add(name, param);
//        }
//        public T GetParam<T>(string paramName) where T : class
//        {
//            if (parameters.ContainsKey(paramName))
//            {
//                return parameters[paramName] as T;
//            }

//            throw new NoSuchParameterException(paramName);
//        }
//        public double GetParamDouble(string paramName)
//        {
//            if (parameters.ContainsKey(paramName))
//                return numberParameters[paramName];
//            throw new NoSuchParameterException(paramName);
//        }

//    }
//}
