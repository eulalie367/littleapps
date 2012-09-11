using System.Configuration;

namespace $rootnamespace$.ConfigurationHelpers
{
    /// <summary>
    /// Represents a collection of generic content elements.
    /// </summary>
    public class GenericContentSettingsCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Creates a new generic content element.
        /// </summary>
        /// <returns>ConfigurationElement object</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new GenericContentElement();
        }

        /// <summary>
        /// Creates a new generic content element based on an element name.
        /// </summary>
        /// <param name="elementName">the name of the generic content element to be created</param>
        /// <returns>ConfigurationElement object</returns>
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new GenericContentElement(elementName);
        }

        /// <summary>
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <param name="element">the ConfigurationElement to return the key for</param>
        /// <returns>object</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GenericContentElement)element).ProviderName;
        }

        /// <summary>
        /// Inserts a new generic content element in the collection.
        /// </summary>
        /// <param name="element">generic content element</param>
        public void Insert(GenericContentElement element)
        {
            base.BaseAdd(element);
        }
    }
}
