import React from "react";
import { StyleSheet, Text, View, ScrollView } from "react-native";
import data from "../data";
import Accordion from "react-native-collapsible/Accordion";
import { FontAwesome } from "@expo/vector-icons";
import { Button } from "react-native-elements";
import Map from './Map'
import {backendUrl} from "../data";
import createOpenLink from 'react-native-open-maps';

export default function Home(navigation, route) {
  const [activeSections, setActiveSections] = React.useState([]);

  const [datas, setDatas] = React.useState(data);

const goToMap = (streetAndNumber,zipCode,city)=>{
    const destinationAddress = streetAndNumber+ ','+ city+','+zipCode;
    createOpenLink({ travelType:'drive',  end: destinationAddress });

  }
  const setSection =(index)=>{
    let newData=[...datas]
    if(activeSections.length>0){
      newData[activeSections[0]].color=''
    }
    if(index.length>0){
      newData[index[0]].color='black'
    }
    setDatas(newData)
    setActiveSections(index)
  }
  const completeJob = async (id) => {
  setDatas(datas[activeSections[0]].ok= false);
      alert("Thank You!!");
       /*try {
        const response = await fetch(`${backendUrl}/swabjob/complete`, {
          method: 'POST',
          headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            Authorization: `Bearer ${global.theToken}`
          },
          body: JSON.stringify({
            jobId: id
          }),
        });
        const {error} = response.json();
        alert(error)
      } catch (e) {
        console.log(e);
        alert(e)

      }*/
    };
  const getJobs = async () => {
 
    try {
      const response = await fetch(`${backendUrl}/swabJobs/forMe`, {
        method: 'GET',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
          Authorization: `Bearer ${global.theToken}`
        }
      });
      if(response.ok){
        const datas = response.json();
        if(datas.length==0){
          alert("No avaliable Jobs..")
        }else{
          sent(datas)
        }
      }else{
        const {error}=response.json()
        alert(error)
      }
      
    } catch (e) {
      console.log(e);
      alert(e)
    }
  };
  const acceptJob = async (id) => {
    alert ("The job is accepted")
    const newData=[...datas]
    newData[activeSections[0]].ok=true
    setDatas(newData);
    // console.log("Accept "+ global.deneme);
    // try {
    //   const response = await fetch(`${backendUrl}/swabJobs/accept`, {
    //     method: 'POST',
    //     headers: {
    //       Accept: 'application/json',
    //       'Content-Type': 'application/json',
    //       Authorization: `Bearer ${global.theToken}`
    //     },
    //     body: JSON.stringify({
    //       jobId: id
    //     }),
    //   });
    //   if(response.ok){
    //     alert("The job is accepted succesfully..")
    //   }else{
    //     const {error}=response.json()
    //     alert(error)
    //   }

    // } catch (e) {
    //   console.log(e);
    //   alert("accept"+e)

    // }
  }
  _renderHeader = section => {
    let color=section.color
    if(!color){
      if(section.ok){
        color='green'
      }else{
        color="#FA1300"
      }
    }
    return (
      <View style={styles.button}>
        <Text style={styles.text}>
          {section.firstname} {section.lastname}
        </Text>
        {section.ok ? (
          <FontAwesome name="check" size={25} color={color} />
        ) : (
          <FontAwesome name="flag-checkered" size={25} color={color} />
        )}
      </View>
    );
  };

  _renderContent = section => {
    return (
      <View style={styles.contentContainer}>
        <Text style={styles.text}> STR : {section.streetAndNumber}</Text>
        <Text style={styles.text}> PLZ : {section.zipCode}</Text>
        <Text style={styles.text}> STADT : {section.city}</Text>
        {!section.ok ? (
          <Button
            buttonStyle={{ padding: 14 }}
            title="Get Job"
            onPress={() => acceptJob(section.id)}
          />
        ) : (
          <View>
            <Button
              buttonStyle={{ margin: 5, padding: 14 }}
              title="Go to Maps"
              onPress={() =>
                goToMap(section.streetAndNumber, section.zipCode, section.city)
              }
            />
            <Button
              buttonStyle={{ padding: 14 }}
              title="I complete job"
              onPress={() => completeJob(section.id)}
            />
          </View>
        )}
      </View>
    );
  };

  return (
    <View style={{ flex: 1 }}>
      <Map style={{ flex: 0.7 }}></Map>
      <ScrollView style={{ flex: 0.3 }}>
        <Accordion
          sections={data}
          activeSections={activeSections}
          renderHeader={_renderHeader}
          renderContent={_renderContent}
          sectionContainerStyle={styles.section}
          onChange={index => {
            setSection(index);
          }}
        />
      </ScrollView>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    width: "90%",
    margin: 15,
    padding: 5,
    alignItems: "center",
    justifyContent: "center",
    borderWidth: 3,
    borderRadius: 10,
    borderColor: "#E4E4E4",
    backgroundColor: "white"
  },
  text: {
    fontSize: 16,
    margin: 10
  },
  textContainer: {
    margin: 20
  },
  button: {
    padding: 15,
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center"
  },
  section: {
    borderColor: "black",
    borderWidth: 2
  },
  buttonStyle: {
    padding: 3
  }
});
