using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace WcfService.Unity.UnityBase
{

    public static class CustomUnityContainer
    {
        /// <summary>
        /// private member instance for unity Container
        /// </summary>
        private static IUnityContainer _container;

        /// <summary>
        /// Property to hold the Unity Container
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public static IUnityContainer Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        /// <summary>
        /// Defination for the Unity Resolve Property.
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <returns>
        /// Object
        /// </returns>
        public static T Resolve<T>()
        {
            if (_container != null)
            {
                if (_container.IsRegistered(typeof(T)))
                {
                    return _container.Resolve<T>();
                }
            }
            return default(T);
        }

        /// <summary>
        /// Defination for the Unity Resolve Property with name property.
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="name">string specifies the name</param>
        /// <returns>
        /// Object
        /// </returns>
        public static T Resolve<T>(string name)
        {
            if (_container.IsRegistered(typeof(T), name))
            {
                return _container.Resolve<T>(name, new ResolverOverride[] { });
            }
            return default(T);
        }

        /// <summary>
        /// Defination for the Unity Register.
        /// </summary>
        /// <param name="from">Type</param>
        /// <param name="to">Type</param>
        public static void Register(Type from, Type to)
        {
            if (_container.Registrations.Any(c => c.MappedToType == to && c.RegisteredType == from) == false)
            {
                _container.RegisterType(from, to, new ContainerControlledLifetimeManager(), new InjectionMember[] { });
            }
        }

        /// <summary>
        /// This generic method has been used to register the objects
        /// </summary>
        /// <typeparam name="T">object map from</typeparam>
        /// <typeparam name="U">object map to</typeparam>
        public static void Register<T, U>()
        {
            _container.RegisterType(typeof(T), typeof(U));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="propertyName"></param>
        public static void Register<T, U>(string propertyName)
        {
            _container.RegisterType(typeof(T), typeof(U), new InjectionProperty(propertyName));
        }

        /// <summary>
        /// To register the instance.
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="name">string specifies the name</param>
        /// <param name="objectToRegister">Object</param>
        public static void RegisterInstance<T>(string name, T objectToRegister)
        {
            _container.RegisterInstance(name, objectToRegister);
        }
    }

}
