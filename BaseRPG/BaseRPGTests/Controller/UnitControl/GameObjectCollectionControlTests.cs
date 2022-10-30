using Xunit;
using BaseRPG.Controller.UnitControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.UnitControl.Tests
{
    public class GameObjectCollectionControlTests
    {
        GameObjectCollectionControl control = new GameObjectCollectionControl();
        
        private void QueueEmpty(int times) {
            for (int i = 0; i < times; i++)
                control.QueueForAdd(null, null, null, null);
        }

        private void QueueAndAddEmpty(int times) {
            for (int i = 0; i < times; i++) {
                QueueEmpty(10);
            }
        }
        [Fact()]
        public void QueueForAddTest()
        {
            QueueEmpty(1);
            //control.AddQueued();
            Assert.Equal(1, control.Count);
        }
        private List<Thread> StartThreads(int numberOfThreads, Action callback)
        {
            List<Thread> threads = new();
            for (int i = 0; i < numberOfThreads; i++)
            {
                threads.Add(new Thread(new ThreadStart(callback)));
            }
            threads.ForEach(t => t.Start());
            return threads;

        }
        [Fact()]
        public void ConcurrentAdd() {
            int addedCounter = 0;
            int queuedCounter = 0;
            control.OnAddCalled += () => ++addedCounter;
            control.OnAddQueueCalled += () => ++queuedCounter;

            var threads = StartThreads(2, () => QueueAndAddEmpty(99999));
            for (int i = 0; i < 99999; i++){
                control.AddQueued();
            }
            threads.ForEach(t => t.Join());
            control.AddQueued();
            Assert.Equal(queuedCounter, addedCounter);
        }

        [Fact()]
        public void ConcurrentAddWithLock()
        {
            int addedCounter = 0;
            int queuedCounter = 0;
            new Thread(() => QueueEmpty(2)).Start();
            new Thread(() => QueueEmpty(2)).Start();
            control.AddQueued();
            Assert.Equal(queuedCounter, addedCounter);
        }
        [Fact()]
        public void AddQueuedTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}