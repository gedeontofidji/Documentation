using Microsoft.Xrm.Sdk;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Cor.Xrm.Plugins.AppCode.ServiceProviders;
using Cor.Xrm.Utilities;
using Cor.Xrm.Utilities.Extensions;

namespace Cor.Xrm.Plugins.AppCode
{
    public abstract class Plugin : Plugin<Entity>
    {
        protected Plugin(string unsecure, string secure)
            : base(unsecure, secure)
        {
        }
    }

    /// <summary>
    /// Classe représentant les traitements réalisés par un plugin
    /// </summary>
    /// <remarks>
    ///
    /// Cette classe ne doit contenir aucun code métier. Elle sert uniquement à
    /// déclencher des événements en fonction d'étape de traitement et de message
    ///
    /// Pour ajouter la gestion d'un message particulier:
    /// - Compléter les switch/case des messages dans la méthode "Execute"
    /// - Ajouter une méthode virtuelle dans la région "Méthodes virtuelles"
    /// - Surcharger la méthode virtuelle dans la classe de plugin dédiée à l'entité concernée
    /// </remarks>
    public abstract class Plugin<T> : IPlugin where T : Entity
    {
        #region Variables

        protected PluginFeature PluginFeature;
        protected bool TraceParentContext;
        private readonly string secureConfiguration;
        private readonly string unsecureConfiguration;

        #endregion Variables

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe ItlPlugin
        /// </summary>
        protected Plugin()
        { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe ItlPlugin
        /// </summary>
        /// <param name="unsecureConfiguration">Données de configuration publiques</param>
        /// <param name="secureConfiguration">Données de configuration non publiques</param>
        protected Plugin(string unsecureConfiguration, string secureConfiguration)
        {
            this.unsecureConfiguration = unsecureConfiguration;
            this.secureConfiguration = secureConfiguration;

            if (!string.IsNullOrEmpty(unsecureConfiguration))
            {
                PluginFeature =
                    SerializerHelper.ReadObject<PluginFeature>(
                        new MemoryStream(Encoding.Default.GetBytes(unsecureConfiguration)));
            }
        }

        #endregion Constructeur

        #region Exécution

        public void Execute(IServiceProvider serviceProvider)
        {
            var esp = new ExtendedServiceProvider<T>(serviceProvider);

            esp.Trace($"Initiating user:{esp.Context.InitiatingUserId}");
            esp.Trace($"User:{esp.Context.UserId}");
            esp.Trace($"Record:{esp.Context.PrimaryEntityId} ({esp.Context.PrimaryEntityName})");

            foreach (var ip in esp.Context.InputParameters)
            {
                if (ip.Value is Entity value)
                {
                    esp.Trace("Input Parameters:");
                    var ipValue = value.ExtractAttributes(null);
                    esp.Trace($"{ip.Key}");
                    esp.Trace(ipValue);
                }
            }

            if (TraceParentContext)
            {
                var parentContext = esp.Context.ParentContext;
                while (parentContext != null)
                {
                    esp.Trace($"PrimaryEntityName: {parentContext.PrimaryEntityName}");
                    esp.Trace($"Message: {parentContext.MessageName}");
                    esp.Trace($"Stage: {parentContext.Stage}");
                    esp.Trace($"Initiating user:{parentContext.InitiatingUserId}");
                    esp.Trace($"User:{parentContext.UserId}");

                    parentContext = parentContext.ParentContext;
                }
            }

            esp.Trace("");
            esp.Trace("Traitements:");

            try
            {
                switch (esp.Context.Stage)
                {
                    case PluginStage.PreValidation:
                        {
                            switch (esp.Context.MessageName)
                            {
                                case PluginMessage.Create:
                                    {
                                        PreValidationCreate(esp);
                                        PreValidationCreate(new CreationServiceProvider<T>(serviceProvider));
                                        break;
                                    }
                                case PluginMessage.Update:
                                    {
                                        PreValidationUpdate(esp);
                                        PreValidationUpdate(new UpdateServiceProvider<T>(serviceProvider));
                                        break;
                                    }
                                case PluginMessage.Delete:
                                    {
                                        PreValidationDelete(esp);
                                        PreValidationDelete(new DeleteServiceProvider<T>(serviceProvider));
                                        break;
                                    }
                                case PluginMessage.SetState: PreValidationSetState(esp); break;
                                case PluginMessage.SetStateDynamicEntity: PreValidationSetStateDynamicEntity(esp); break;
                                default: DefaultPreValidation(esp); break;
                                    // Ajouter ici les messages supplémentaires à gérer
                            }
                        }
                        break;

                    case PluginStage.PreOperation:
                        {
                            switch (esp.Context.MessageName)
                            {
                                case PluginMessage.Create:
                                    {
                                        PreOperationCreate(esp);
                                        PreOperationCreate(new CreationServiceProvider<T>(serviceProvider));
                                        break;
                                    }
                                case PluginMessage.Update:
                                    {
                                        PreOperationUpdate(esp);
                                        PreOperationUpdate(new UpdateServiceProvider<T>(serviceProvider));
                                        break;
                                    }
                                case PluginMessage.Delete:
                                    {
                                        PreOperationDelete(esp);
                                        PreOperationDelete(new DeleteServiceProvider<T>(serviceProvider));
                                        break;
                                    }
                                case PluginMessage.QualifyLead:
                                    {
                                        PreOperationQualifyLead(new QualifyServiceProvider<T>(serviceProvider));
                                        break;
                                    }
                                default: DefaultPreOperation(esp); break;

                                    // Ajouter ici les messages supplémentaires à gérer
                            }
                        }
                        break;

                    case PluginStage.MainOperation:
                        {
                            switch (esp.Context.MessageName)
                            {
                                default: DefaultMainOperation(esp); break;
                            }
                        }
                        break;

                    case PluginStage.PostOperation:
                        {
                            switch (esp.Context.MessageName)
                            {
                                case PluginMessage.Create:
                                    if (esp.Context.Mode == PluginMode.Synchronous)
                                    {
                                        PostOperationCreate(esp);
                                        PostOperationCreate(new CreationServiceProvider<T>(serviceProvider));
                                    }
                                    else
                                    {
                                        PostOperationCreateAsync(esp);
                                        PostOperationCreateAsync(new CreationServiceProvider<T>(serviceProvider));
                                    }
                                    break;

                                case PluginMessage.Update:
                                    if (esp.Context.Mode == PluginMode.Synchronous)
                                    {
                                        PostOperationUpdate(esp);
                                        PostOperationUpdate(new UpdateServiceProvider<T>(serviceProvider));
                                    }
                                    else
                                    {
                                        PostOperationUpdateAsync(esp);
                                        PostOperationUpdateAsync(new UpdateServiceProvider<T>(serviceProvider));
                                    }
                                    break;

                                case PluginMessage.Delete:
                                    {
                                        PostOperationDelete(esp);
                                        PostOperationDelete(new DeleteServiceProvider<T>(serviceProvider));
                                        break;
                                    }
                                case PluginMessage.SetState: PostOperationSetState(esp); break;
                                case PluginMessage.SetStateDynamicEntity: PostOperationSetStateDynamicEntity(esp); break;
                                case PluginMessage.Associate:
                                    {
                                        PostOperationAssociate(esp);
                                        PostOperationAssociate(new RelationshipServiceProvider<T>(serviceProvider));
                                        break;
                                    }
                                case PluginMessage.Disassociate:
                                    {
                                        PostOperationDisassociate(esp);
                                        PostOperationDisassociate(new RelationshipServiceProvider<T>(serviceProvider));
                                        break;
                                    }
                                case PluginMessage.AddUserToRecordTeam: PostAddUserToRecordTeam(esp); break;
                                case PluginMessage.RemoveUserFromRecordTeam: PostRemoveUserFromRecordTeam(esp); break;
                                case PluginMessage.Lose: PostOperationLose(new OpportunityCloseServiceProvider<T>(serviceProvider)); break;
                                case PluginMessage.Win: PostOperationWin(new OpportunityCloseServiceProvider<T>(serviceProvider)); break;
                                case PluginMessage.QualifyLead:
                                    {
                                        PostOperationQualifyLead(new QualifyServiceProvider<T>(serviceProvider));
                                        break;
                                    }
                                default: DefaultPostOperation(esp); break;

                                    // Ajouter ici les messages supplémentaires à gérer
                            }
                        }
                        break;
                }
            }
            catch (Exception error)
            {
                esp.TraceError(error.ToString());

                throw new InvalidPluginExecutionException(error.Message, error);
            }
        }

        #endregion Exécution

        #region Méthodes virtuelles

        public virtual void DefaultMainOperation(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void DefaultPostOperation(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void DefaultPreOperation(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void DefaultPreValidation(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PostAddUserToRecordTeam(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PostOperationAssociate(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PostOperationAssociate(RelationshipServiceProvider<T> rsp)
        {
        }

        public virtual void PostOperationCreate(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PostOperationCreate(CreationServiceProvider<T> csp)
        {
        }

        public virtual void PostOperationCreateAsync(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PostOperationCreateAsync(CreationServiceProvider<T> csp)
        {
        }

        public virtual void PostOperationDelete(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PostOperationDelete(DeleteServiceProvider<T> dsp)
        {
        }

        public virtual void PostOperationDisassociate(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PostOperationDisassociate(RelationshipServiceProvider<T> rsp)
        {
        }

        public virtual void PostOperationLose(OpportunityCloseServiceProvider<T> esp)
        {
        }

        public virtual void PostOperationQualifyLead(QualifyServiceProvider<T> qualifyServiceProvider)
        {
        }

        public virtual void PostOperationSetState(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PostOperationSetStateDynamicEntity(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PostOperationUpdate(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PostOperationUpdate(UpdateServiceProvider<T> usp)
        {
        }

        public virtual void PostOperationUpdateAsync(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PostOperationUpdateAsync(UpdateServiceProvider<T> usp)
        {
        }

        public virtual void PostOperationWin(OpportunityCloseServiceProvider<T> esp)
        {
        }

        public virtual void PostRemoveUserFromRecordTeam(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PreOperationCreate(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PreOperationCreate(CreationServiceProvider<T> csp)
        {
        }

        public virtual void PreOperationDelete(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PreOperationDelete(DeleteServiceProvider<T> dsp)
        {
        }

        public virtual void PreOperationQualifyLead(QualifyServiceProvider<T> qualifyServiceProvider)
        {
        }

        public virtual void PreOperationUpdate(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PreOperationUpdate(UpdateServiceProvider<T> usp)
        {
        }

        public virtual void PreValidationCreate(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PreValidationCreate(CreationServiceProvider<T> csp)
        {
        }

        public virtual void PreValidationDelete(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PreValidationDelete(DeleteServiceProvider<T> dsp)
        {
        }

        public virtual void PreValidationSetState(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PreValidationSetStateDynamicEntity(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PreValidationUpdate(ExtendedServiceProvider<T> esp)
        {
        }

        public virtual void PreValidationUpdate(UpdateServiceProvider<T> usp)
        {
        }

        #endregion Méthodes virtuelles

        #region Méthodes

        /// <summary>
        /// Obtient la valeur de configuration non publique
        /// </summary>
        /// <returns>Valeur de configuration non publique</returns>
        public string GetSecureConfiguration()
        {
            return secureConfiguration;
        }

        /// <summary>
        /// Obtient la valeur de configuration publique
        /// </summary>
        /// <returns>Valeur de configuration publique</returns>
        public string GetUnsecureConfiguration()
        {
            return unsecureConfiguration;
        }

        /// <summary>
        /// Indique si une fonctionnalité doit être activée ou pas. Sans configuration trouvée
        /// dans l'étape de traitement de plugin, la fonctionnalité est considérée comme activée
        /// </summary>
        /// <remarks>
        /// La configuration des fonctionnalités se fait en passant un json comme ci dessous
        /// dans la configuration non sécurisée du plugin:
        /// {"Features":[{"Enabled":true,"Name":"feature1"},{"Enabled":false,"Name":"feature2"}]}
        /// </remarks>
        /// <param name="featureName">Nom de la fonctionnalité</param>
        /// <returns>Indicateur d'état</returns>
        protected bool IsFeatureEnabled(string featureName)
        {
            return PluginFeature?.Features.Any(f => f.Name == featureName && f.Enabled) ?? true;
        }

        #endregion Méthodes
    }
}