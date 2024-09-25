  Sends a text message containing the Roll of Roof (ROR) and Telescope Mount status. 
   
  Used in Sequence Generator Pro (SGPro).
  In SGPro under 'Sequence Setting' then 'Run script at sequence end:' select
  the SGProTexter.cmd. The cmd is going to launch SGProTexter.exe in another 
  but separate terminal (windows 11) window so SGPro can continue. 
  Then SGProTexter will get the status of the ROR and Mount and text this.
   
 Email is used and is stored encrypted using Windows user credentials to protect 
 you email password. 
 You can set to always send the text with ROR and Mount status or only send the 
 text if either the mount didn’t park or the ROR is not closed.
 There is a camera warmup delay. SGPro will run the end of script before it waits for 
 the camera to warm up so the SGProTexter will delay and then send  the text .
 This help to avoid getting a text early AM only having to wait minutes will the 
 camera is warming up in other to power down equipment.
