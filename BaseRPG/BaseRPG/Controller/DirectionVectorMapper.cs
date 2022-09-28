using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller
{
    public class DirectionVectorMapper
    {
        private Dictionary<MoveDirection, Vector2D> map;

        public DirectionVectorMapper(Dictionary<MoveDirection, Vector2D> map)
        {
            this.map = map;
        }
        public static DirectionVectorMapper CreateDefault() {
            DirectionVectorMapper directionVectorMapper = new DirectionVectorMapper(new Dictionary<MoveDirection, Vector2D>());
            directionVectorMapper.map.Add(MoveDirection.Left, new Vector2D(-1, 0));
            directionVectorMapper.map.Add(MoveDirection.Right, new Vector2D(1, 0));
            directionVectorMapper.map.Add(MoveDirection.Forward, new Vector2D(0, -1));
            directionVectorMapper.map.Add(MoveDirection.Backward, new Vector2D(0, 1));
            return directionVectorMapper;
        }

        public Vector2D FromDirection(MoveDirection moveDirection)
        {
            return map[moveDirection];
        }
    }
}
