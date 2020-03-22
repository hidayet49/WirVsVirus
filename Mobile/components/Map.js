import React,{ useRef } from "react";
import MapView, { Marker } from 'react-native-maps';
import { StyleSheet, Text, View, PermissionsAndroid,Platform } from "react-native";

function Map({ navigation }) {
  const [currentLocation, setCurrentLocation] = React.useState({latitude: 0,longitude: 0,});
  const mapRef = useRef();  
  
  React.useEffect(() => {
      // Fetch the token from storage then navigate to our appropriate place
      const fetchLocation = ()=> {
        navigator.geolocation.getCurrentPosition((position) => {
        console.log(position)
        var lat = parseFloat(position.coords.latitude)
        var long = parseFloat(position.coords.longitude)
  
        var currentLocation = {
          latitude: lat,
          longitude: long,
        }
  
        setCurrentLocation(currentLocation)
        mapRef.current.fitToCoordinates([currentLocation],{
          edgePadding: { top: 120, right: 120, bottom: 120, left: 120 },
          animated: true,
        });
      },
      (error) => alert(JSON.stringify(error)),
      {enableHighAccuracy: true, timeout: 20000, maximumAge: 1000});
    }

    if (Platform.OS === 'android') {
      PermissionsAndroid.requestPermission(
        PermissionsAndroid.PERMISSIONS.ACCESS_FINE_LOCATION
      ).then(granted => {
        if (granted) {
          fetchLocation();
        }
      });
    } else {
      fetchLocation();
    }
    }, []);

  return (
    <View style={styles.rootContainer}>
    <MapView 
    ref={mapRef}
    style={styles.mapStyle} 
    showsUserLocation = {true}
    showsMyLocationButton ={true}
    provider ={"google"}
    initialRegion = {currentLocation}
    
    >
<Marker coordinate={currentLocation}>
  <View style={styles.container}>
          <View style={styles.markerHalo} />
            <View style={[styles.heading]}>
              <View style={styles.headingPointer} />
            </View>
          <View style={styles.marker}>
          </View>
        </View>
        </Marker>

    </MapView>

  </View>
  );
}

const SIZE = 35;
const HALO_RADIUS = 6;
const ARROW_SIZE = 7;
const ARROW_DISTANCE = 6;
const HALO_SIZE = SIZE + HALO_RADIUS;
const HEADING_BOX_SIZE = HALO_SIZE + ARROW_SIZE + ARROW_DISTANCE;
const styles = StyleSheet.create({
    rootContainer: {
        flex: 1,
        backgroundColor: '#fff',
      },
      mapStyle: {
        flex: 1,
      },
      mapMarker: {
        zIndex: 1000,
      },
      // The container is necessary to protect the markerHalo shadow from clipping
      container: {
        width: HEADING_BOX_SIZE,
        height: HEADING_BOX_SIZE,
      },
      heading: {
        position: 'absolute',
        top: 0,
        left: 0,
        width: HEADING_BOX_SIZE,
        height: HEADING_BOX_SIZE,
        alignItems: 'center',
      },
      headingPointer: {
        width: 0,
        height: 0,
        backgroundColor: 'transparent',
        borderStyle: 'solid',
        borderTopWidth: 0,
        borderRightWidth: ARROW_SIZE * 0.75,
        borderBottomWidth: ARROW_SIZE,
        borderLeftWidth: ARROW_SIZE * 0.75,
        borderTopColor: 'transparent',
        borderRightColor: 'transparent',
        borderBottomColor: '#3399ff',
        borderLeftColor: 'transparent',
      },
      markerHalo: {
        position: 'absolute',
        backgroundColor: 'white',
        top: 0,
        left: 0,
        width: HALO_SIZE,
        height: HALO_SIZE,
        borderRadius: Math.ceil(HALO_SIZE / 2),
        margin: (HEADING_BOX_SIZE - HALO_SIZE) / 2,
        shadowColor: 'black',
        shadowOpacity: 0.25,
        shadowRadius: 2,
        shadowOffset: {
          height: 0,
          width: 0,
        },
      },
      marker: {
        justifyContent: 'center',
        backgroundColor: '#3399ff',
        width: SIZE,
        height: SIZE,
        borderRadius: Math.ceil(SIZE / 2),
        margin: (HEADING_BOX_SIZE - SIZE) / 2,
      },
});


export default Map