# Calculator Project

![Calculator](https://github.com/Ilya-ekb/Calculator/blob/master/Assets/Resources/UI/calc-img.png)

## Description

This project is a simple calculator implemented using the MVP (Model-View-Presenter) design pattern. The application supports various models, storages, and views, allowing for easy extension and modification of its functionality.

### Features
- Simple addition model
- Support for file and PlayerPrefs storage
- UI Toolkit-based user interface
- History of calculations

### Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/Ilya-ekb/AwesomeCalculator.git
   ```
2. Open the project in Unity.
3. Open the SampleScene
4. Entry point is the MonoCalculator.cs component, which has a field that accepts a presenter configurator (a subclass of ScriptableObject.cs). Depending on the selected configurator, you can change the initialized model, view, and data storage mechanism.
   
   ![MonoCalculator](https://github.com/Ilya-ekb/Calculator/blob/master/Assets/Resources/editor.png)

6. Each configurator has three fields:
     - Model - configuration of the model
     - Storage - configuration of the storage
     - View - configuration of the view]
       
   Depending on the chosen configuration, the presenter can be flexibly configured.
   
   ![PresenterConfig](https://github.com/Ilya-ekb/Calculator/blob/master/Assets/Resources/presenter.png)

7. Run the Play Mode.

### Technologies Used
- Unity
- C#
- Newtonsoft.Json for data serialization

### Contributing
Contributions are welcome! Please open an issue or submit a pull request.

## Project Structure
The project consists of the following main components:
1. **Models (CalculatorModels)**
2. **Storages (CalculatorStorages)**
3. **Views (CalculatorViews)**
4. **Presenters (CalculatorPresenters)**
5. **Configurations (MonoScripts)**

### 1. Models (CalculatorModels)

#### Interface ICalculatorModel
```csharp
public interface ICalculatorModel
{
    string Calculate(string input);
}
```
- **Purpose:** Defines the `Calculate` method, which takes a `string input` and returns a `string` result of the calculation.

### 2. Storages (CalculatorStorages)

#### Interface IStorage
```csharp
public interface IStorage
{
    string LastValue { get; set; }
    string History { get; }
    void Load();
    void Save();
    void UpdateHistory(string value);
    void ClearHistory();
}
```
- **Purpose:** Defines methods for storing and managing values and calculation history.

#### Class FileStorage
```csharp
public class FileStorage : BaseStorage
{
    // Implementation of saving and loading data to/from a file
}
```
- **Purpose:** Stores values and history of calculations in a text file using JSON format.

#### Class PrefsStorage
```csharp
public class PrefsStorage : BaseStorage
{
    // Saving and loading data using PlayerPrefs
}
```
- **Purpose:** Stores values and history of calculations using Unity's PlayerPrefs.

### 3. Views (CalculatorViews)

#### Interface ICalculatorView
```csharp
public interface ICalculatorView
{
    event Action<string> OnChangeInput;
    event Action OnExecuteInteraction; 
    event Action<string> OnResultExecuted;
    event Action OnClearHistoryInteraction;
    void SetInput(string input);
    string GetInput();
    void DisplayResult(string result);
    void DisplayHistory(string history);
}
```
- **Purpose:** Defines methods and events used for interacting with the user interface.

#### Class CalculatorUIToolkitView
```csharp
public class CalculatorUIToolkitView : BaseCalculatorView
{
    // Implementation of user interface using UI Toolkit
}
```
- **Purpose:** Manages the user interface of the calculator created using the UI Toolkit. Handles interaction events and displays results.

### 4. Presenters (CalculatorPresenters)

#### Class CalculatorPresenter
```csharp
public class CalculatorPresenter
{
    // Manages the logic for interaction between model, storage, and view
}
```
- **Purpose:** Links the calculator model, storage, and view. Handles events, performs calculations, and manages the application state.

### 5. Configurations (MonoScripts)

#### Class CalculatorPresenterConfig
```csharp
public class CalculatorPresenterConfig : ScriptableObject
{
    // Configuration for creating an instance of CalculatorPresenter
}
```
- **Purpose:** Specifies parameters for creating an instance of `CalculatorPresenter`, including model, storage, and view. Checks for the presence of all necessary components.

#### Class FileStorageConfig
```csharp
public class FileStorageConfig : StorageConfig
{
    // Configuration for creating an instance of FileStorage
}
```
- **Purpose:** Allows selection of the directory path for file storage and creates an instance of `FileStorage`.

#### Class PrefsStorageConfig
```csharp
public class PrefsStorageConfig : StorageConfig
{
    // Configuration for creating an instance of PrefsStorage
}
```
- **Purpose:** Specifies the key of preferences for storing data using PlayerPrefs.

#### Class MonoCalculator
```csharp
public class MonoCalculator : MonoBehaviour
{
    // Component for managing the lifecycle of the calculator in Unity
}
```
- **Purpose:** Initializes and removes the instance of `CalculatorPresenter` based on the component's state.

#### Abstract Class ModelConfig
```csharp
public abstract class ModelConfig : ScriptableObject
{
    public abstract ICalculatorModel GetModel();
}
```
- **Purpose:** Defines a method for obtaining an instance of the calculator model.

#### Class OnlyAddModel
```csharp
public class OnlyAddModel : ModelConfig
{
    public override ICalculatorModel GetModel()
    {
        return new OnlyAddCalculator();
    }
}
```
- **Purpose:** Implements the `ModelConfig` interface by providing a model that performs only addition operations.

#### Abstract Class StorageConfig
```csharp
public abstract class StorageConfig : ScriptableObject
{
    public abstract IStorage GetStorage();
}
```
- **Purpose:** Provides a method for obtaining an instance of storage.

#### Abstract Class ViewConfig
```csharp
public abstract class ViewConfig : ScriptableObject
{
    public abstract ICalculatorView GetView();
}
```
- **Purpose:** Defines a method for creating an instance of the calculator view.

#### Class UIToolkitViewConfig
```csharp
public class UIToolkitViewConfig : ViewConfig
{
    // Configuration for creating an instance of UIToolkitView
}
```
- **Purpose:** Creates an instance of `CalculatorUIToolkitView` using the specified template and panel settings.

## Notes

- The code uses the MVP (Model-View-Presenter) design pattern to separate application logic into models, views, and presenters.
- The project utilizes the Newtonsoft.Json library for data serialization.
- Each view must implement the `ICalculatorView` interface to be compatible with the presenter.
