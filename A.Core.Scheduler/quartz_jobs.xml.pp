<?xml version="1.0" encoding="UTF-8"?>

<!-- This file contains job definitions in schema version 2.0 format -->

<job-scheduling-data xmlns="http://quartznet.sourceforge.net/JobSchedulingData" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" version="2.0">

  <processing-directives>
    <overwrite-existing-data>true</overwrite-existing-data>
  </processing-directives>
  <schedule>
    <job>
      <name>heartbeatJobName</name>
      <group>all</group>
      <description>Heartbeat job - used for testing</description>
      <job-type>$rootnamespace$.Jobs.HeartbeatJob, $rootnamespace$</job-type>
      <durable>true</durable>
      <recover>false</recover>
    </job>


    <trigger>
      <simple>
        <name>RunHeartbeatJobTrigger</name>
        <group>all</group>
        <description>Trigger to fire lookup update</description>
        <job-name>heartbeatJobName</job-name>
        <job-group>all</job-group>
        <misfire-instruction>SmartPolicy</misfire-instruction>
        <repeat-count>-1</repeat-count>
        <repeat-interval>5000</repeat-interval>
      </simple>
    </trigger>

  </schedule>
  
</job-scheduling-data>