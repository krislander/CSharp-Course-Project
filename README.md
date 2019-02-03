# CSharp-Course-Project
Course Project
Обектно Ориентирано Програмиране
(OOP with C#.NET)
Име на Проекта: ИНФОРМАЦИОННА СИСТЕМА ЗА МОНИТОРИНГ И АНАЛИЗ УСЛОВИЯТА ЗА
ЖИВОТ В СТУДЕНТСКИ ОБЩЕЖИТИЯ
Acceptable Programming Languages: C#.NET
Deadline: Февруари, 2018(on the final exam date)
Instructor Dr. Evgeny Krustev
Problem Statement:
1 Project description
ICB received a request form a client to develop an information system that collects data from various
sensors located in college dormitories all over the world. Data will be analyzed by independent research
organization to evaluate the living conditions and map them to the performance of the students.
Participants should have installed specific sensor equipment supplied by the organization and register to
smartDormitory.
2 Sensor types
The following sensor types should be supported:
● Temperature, measured in °C
● Humidity sensor, measured in percent
● Electric power consumption sensor, measured in Watts
● Window /door sensor. Allowed values: true/false(True – when the window/door is open; False –
when the window door is closed)
● Noise sensor, measured in Decibels
Note: ICB will provide web API to fetch sensors data.
3 Client GUI
The Graphical User Interface should support the following functionality:
3.1 Landing page
Provide relevant information about the system, its purpose and features. This page should also include a
map with all sensors (see 3.6) and access to register new sensor (3.2), modify existing sensor (3.3), View
all sensors (3.4), Graphical representation of the sensors (3.5) forms.
3.2 Register new sensor
The newly created sensor should have:
● Name
● Description
● Sensor Type
● Polling interval which specifies the amount of time to refresh sensor data (Optional)
● Latitude and longitude
● Range of acceptable values (e.g. -40 °C to +100 °C)
● Tick-off option for receiving sensor alarm (Out of acceptable range)
3.3 Modify existing sensor
The user should be able to edit sensors.
3.4 View list of all sensors
The user should see a list of registered sensors. For each sensor the list should include:
● Name
● Description
● Current value
● Link to graphical representation of the sensor
This page should contain a link to “View sensors on a map” (see 3.6).
3.5 Graphical representation of sensor
The user should have a detailed sensor’s view which includes a graphical representation of data
collected from sensor.
Note: The system should handle when the sensor is offline and show this to the users
State indicator – examples
3.6 View sensors on a map
This page should provide a map view with sensor markers according to their lat/long. You can use
Google Maps API and Marker clustering library.
On the landing page display all sensors for the users.
3.7 Reports
On this view, the user should be able to define:
- time period (start, end) (Max time period 1 day)
- one or more sensors from the list of all Sensors
- sensor Alarm (out of acceptable range) or all data (mark the out of range values – Optional)
The user should be able to trigger generation of the report based on the selected criteria
The report should be a csv format contents follow information
Timestamp, sensor-name, sensor-description, value, unit of measure, state
Example:
23/11/2018 10:00:01, SST-51-temp, sofia-student-town-bl.51-temperature, 20 deg.
Note the comma in the sensor name, sensor description should be escaped/replaced with “_”
4 General requirements
Use the following technologies, frameworks and development techniques:
1. Use WPF for the GUI
2. Data storage
a. Use XML file format to store sensors definition
3. Graphical representation of sensor data
a. Use Telerik WPF (https://demos.telerik.com/wpf/)
https://www.telerik.com/login/v2/downloadb?ReturnUrl=https%3a%2f%2fwww.telerik.com%2fdownload-trial-file%2fv2-b%2fui-forwpf%3futm_medium%3dtelerik%26utm_source%3dqsf%26utm_campaign%3dwpftrial#register
4. Apply error handling and client data validation to avoid crashes when invalid data is entered
5. Documentation of the project and project architecture (including screenshots)
6. Use caching of data where it makes sense (e.g. landing page) (optional)

