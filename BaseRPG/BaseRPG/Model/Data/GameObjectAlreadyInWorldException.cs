﻿using BaseRPG.Model.Interfaces;
using System;
using System.Runtime.Serialization;

namespace BaseRPG.Model.Data
{
    [Serializable]
    internal class GameObjectAlreadyInWorldException : Exception
    {
        private GameObject gameObjectContainer;

        public GameObjectAlreadyInWorldException()
        {
        }

        public GameObjectAlreadyInWorldException(GameObject gameObjectContainer)
        {
            this.gameObjectContainer = gameObjectContainer;
        }

        public GameObjectAlreadyInWorldException(string message) : base(message)
        {
        }

        public GameObjectAlreadyInWorldException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameObjectAlreadyInWorldException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}