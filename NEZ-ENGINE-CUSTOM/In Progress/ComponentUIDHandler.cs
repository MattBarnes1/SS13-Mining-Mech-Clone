using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEZ_ENGINE_CUSTOM.ECS.Components
{
    public class ComponentUIDHandler
    {

        private static Queue<Guid> _objectQueue = new Queue<Guid>(10);

        public ComponentUIDHandler()
        {
            warmCache(100);
        }

        /// <summary>
        /// warms up the cache filling it with a max of cacheCount objects
        /// </summary>
        /// <param name="cacheCount">new cache count</param>
        public static void warmCache(int cacheCount)
        {
            cacheCount -= _objectQueue.Count;
            if (cacheCount > 0)
            {
                for (var i = 0; i < cacheCount; i++)
                {
                    var retval = Guid.NewGuid();
                    while ((!_objectQueue.Contains(retval)))
                    {
                        retval = Guid.NewGuid();
                    }
                    _objectQueue.Enqueue(retval);
                }
                
            }
        }


        /// <summary>
        /// trims the cache down to cacheCount items
        /// </summary>
        /// <param name="cacheCount">Cache count.</param>
        public static void trimCache(int cacheCount)
        {
            while (cacheCount > _objectQueue.Count)
                _objectQueue.Dequeue();
        }


        /// <summary>
        /// clears out the cache
        /// </summary>
        public static void clearCache()
        {
            _objectQueue.Clear();
        }


        /// <summary>
        /// pops an item off the stack if available creating a new item as necessary
        /// </summary>
        public static Guid obtain()
        {
            if (_objectQueue.Count > 0)
                return _objectQueue.Dequeue();
            var retval = Guid.NewGuid();
            while ((!_objectQueue.Contains(retval)))
            {
                retval = Guid.NewGuid();
            }
            return retval;
        }


        /// <summary>
        /// pushes an item back on the stack
        /// </summary>
        /// <param name="obj">Object.</param>
        public static void free(Guid obj)
        {
            _objectQueue.Enqueue(obj);           
        }
    }
}
