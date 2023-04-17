# GBM Challenge-BackEnd

## **Broker** 🚀

### **Arquitectura** 📌

---

La arquitectura hexagonal proporciona una base sólida para cumplir con varios atributos de calidad importantes en un sistema de software, como la **escalabilidad**, **extensibilidad** y **mantenibilidad** esta última incluye **testeabilidad**.

Es por eso que decidí utilizar la arquitectura hexagonal para desarrollar este proyecto debido a sus numerosas ventajas en términos de modularidad, escalabilidad y facilidad de mantenimiento.

La estructura de la arquitectura hexagonal es altamente modular, lo que significa que cada componente se puede probar de manera aislada.

Además, la arquitectura hexagonal minimiza las dependencias entre diferentes partes del sistema, lo que lo hace más fácil de mantener y evolucionar con el tiempo.

Por lo que el diagrama para el proyecto queda de la siguiente manera:

![Diagrama de Arquitectura Hexagonal](https://firebasestorage.googleapis.com/v0/b/website-bf.appspot.com/o/Wiki%2FDiagram-Hexagonal-Architecture.png?alt=media&token=7b5cfda0-aa22-4d87-ba2e-78d19df3a266)

### **Principios** 📌

---

#### **SOLID**

Al utilizar la arquitectura hexagonal en el desarrollo, estoy siguiendo los principios **SOLID**.

En particular, estoy aplicando el **principio de responsabilidad única** al separar las diferentes capas de mi aplicación.

Ademas estoy aplicando el **principio de abierto cerrado** para asegurar que las diferentes capas de mi aplicación estén desacopladas y sean fácilmente intercambiables. De esta manera, estoy asegurando la extensibilidad y mantenibilidad de mi código.

También estoy aplicando el **principio de inversión de dependencias** al asegurarme de que los módulos no dependan directamente de implementaciones sino de abstracciones.

#### **DRY**

En mi proyecto, estoy siguiendo el principio **DRY** al asegurarme de que cada parte del código tenga una única fuente de verdad y que los conceptos comunes se abstraigan en componentes reutilizables.

#### **KISS**

En mi proyecto, estoy siguiendo el principio **KISS** al evitar la creación de soluciones complejas. En su lugar, estoy enfocándome en soluciones simples y elegantes que sean fáciles de entender y mantener.

### **Solución para las reglas de negocio** 📌

Al utilizar patrones de diseño, establecí contratos bien definidos con una interfaz clara para la implementación de reglas sin romper los principios de la programación **SOLID**, **DRY**, **KISS**.

Para esto identifiqué que hay dos tipos de reglas base, al menos para llevar a cabo operaciones de compra/venta, pero al existir un contrato base, puede crearse un subcontrato fácilmente.

Los cuales son:

- Reglas que para ser validadas solo requieren, el dato de entrada, por ejemplo, **la regla para validar si el mercado está cerrado.**
- Reglas que para ser validadas requieren de información adicional a la entrada, por ejemplo, **la regla para validar si se tiene balance suficiente para comprar**, en este caso como dato adicional se requiere el balance actual.

Toda regla debe implementar el contrato `IBusinessRule`, de acuerdo a lo mencionado anteriormente existen dos subcontratos `BusinessRule` el cual no requiere información adicional y `DependentBusinessRule` el cual requiere información adicional.

Esto quiere decir que a partir de estos subcontratos podemos tener reglas concretas, por ejemplo, `ClosedMarketRule` en este caso implementa `BusinessRule` o `InsufficientBalanceRule` que implementa `DependentBusinessRule`.

Para la ejecución de las reglas se sigue el mismo principio, con un contrato base llamado `Policy` que es el encargado de ejecutar las reglas, de esto podemos generar ejecutores de reglas concretos como `OrderPolicy`, que añade la funcionalidad para validar si una regla puede ser ejecutada en cierto contexto, sean los contextos compra y venta.

De esta manera aseguramos la **extensibilidad** y **escalabilidad**, ya que si hubiera una nueva regla, en ningún momento se rompería el principio de abierto cerrado, simplemente implementamos esa regla y se le asigna, ya sea a una orden de compra o venta.

### **Estrategia de testing** 📌

---

#### **Objetivos de pruebas**

- Asegurarse de que la aplicación sea funcional y esté libre de errores.
- Garantizar que se cumplan las reglas del negocio.

#### **Tipos de Pruebas**

- Pruebas unitarias: para asegurarse de que cada componente del dominio de la aplicación funcione correctamente de manera aislada.
- Pruebas de aceptación: Para determinar si la aplicación cumple con los requisitos del negocio.

#### **Metodología de prueba**

- Se realizará la ejecución de las pruebas cada vez que se integre código al repository para validar que siga funcionando correctamente.

#### **Criterios de aceptación**

- Se considerará que la prueba es exitosa si la aplicación cumple con los requisitos y se ha demostrado que funciona correctamente.
- Se considerará que la prueba es fallida si la aplicación no cumple con los requisitos y si se encuentran errores o problemas en la aplicación.

## **Base de Datos**

Se opto por usar SQL Server para la aplicación debido a que es una plataforma de base de datos sólida, confiable y escalable que ofrece un alto rendimiento para las aplicaciones empresariales. Ademas que se integra muy bien con el ambiente .NET.

El diagrama ER para el proyecto queda de la siguiente manera:

![Diagrama ER](https://firebasestorage.googleapis.com/v0/b/website-bf.appspot.com/o/Wiki%2FDiagrams-Entity%20Relation.png?alt=media&token=d6f8335c-e32a-4980-beb1-a72576687556)
