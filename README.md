<p align="center">
 <h2 align="center">FilterFileManipulation</h2>
 <p align="center">C# Class library project. To perform actions like COPY, MOVE, DELETE under given file extension</b></p>
 <br/>
 <p align="center">
 <img src="https://img.shields.io/github/stars/chandru415/FilterFileManipulation?style=for-the-badge" />
 <img src="https://img.shields.io/github/watchers/chandru415/FilterFileManipulation?style=for-the-badge" />
  <a href="https://www.nuget.org/packages/FFManipulation/">
   <img src="https://img.shields.io/nuget/dt/FFManipulation?style=for-the-badge" />
 </a>
 </p>
</p>
<br/>

---

 **FilterFileManipulation** gives the ability to perform action like *copy*, *move*, *delete* on file(s) based on the desire *file extension*. 

To install the ***package*** to any .Net Framework or .Net Core application please click [here](https://www.nuget.org/packages/FFManipulation/).

<h5>Use case: Filter File Manipulation </h5>

<p> let we have to move(cut) only documents(.docx) file from one folder to another folder.

To achieve this...</p>

<br />

**Source folder**

--- 

<img align="center" src="./assets/sourcefolder.PNG" alt="nuget package image">

<br />


**Destination folder**

--- 

<img align="center" src="./assets/destination.PNG" alt="nuget package image">

<br />

<h4> steps </h4>

* Create .Net Core Console Application
* Under Project dependencies - add **FFManipulation** from nuget package manager

<img align="center" src="./assets/install.PNG" alt="nuget package image">

* Provide required details/options

<img align="center" src="./assets/userinput.PNG" alt="user input image">

* on screen

<img align="center" src="./assets/outuserinput.PNG" alt="out user input">


* Application will process the request and returns result with status as *true* if sucessfully processed other *false* along with *count* of number files were manipluated.


<img align="center" src="./assets/pout.PNG" alt="process output">

* And application will also provide details logs of the manipulation we can find those details on **C:\FFMLogs**


<img align="center" src="./assets/logs.PNG" alt="logs output image">

<br />


**Source folder**

--- 

<img align="center" src="./assets/osourcefolder.PNG" alt="nuget package image">

<br />


**Destination folder**

--- 

<img align="center" src="./assets/odestination.PNG" alt="nuget package image">

<br />


<div align="center">

### Show some ❤️ by starring some of the repositories!

</div>