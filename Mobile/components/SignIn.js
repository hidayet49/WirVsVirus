import React from "react";

import { FontAwesome } from "@expo/vector-icons";
import { Input } from "react-native-elements";
import { TextWithIcon } from "./textWithIcon";
import { StyleSheet, Text, View, TextInput, Button } from "react-native";
import * as SecureStore from "expo-secure-store";
import { TouchableOpacity } from "react-native-gesture-handler";

function SignIn({ navigation }) {
  const [username, setUsername] = React.useState("");
  const [password, setPassword] = React.useState("");
  const [token, setToken] = React.useState("")

  const signIn = async () => {
    try {
      const response = await fetch('https://localhost:5001/api/account/auth', {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email: username,
          password: password,
        }),
      });
      myToken = response.headers.get('Authorization')
      if (myToken) {
        setToken(myToken)
        SecureStore.setItemAsync("userToken", myToken);
        alert(token)
        navigation.navigate('Home', { token: token });
      }
    } catch (e) {
      console.log(e);
      alert(e)

    }
  };

  return (
    <View style={styles.container}>
      <View style= {styles.titleContainer}>
    <Text style={styles.titleText}>Collectify!</Text>
      </View>
      <View style= {styles.loginFormContainer}>
      <Input inputContainerStyle = {styles.input}
        containerStyle ={{marginTop: 12, marginBottom:8}}
        inputStyle ={styles.textInputStyle}
        placeholder="Username"
        value={username}
        placeholderTextColor={'white'}
        leftIconContainerStyle={{ margin: 8 }}
        leftIcon={<FontAwesome name="user" size={24} color="white" />}
        onChangeText={e => setUsername(e)}
      />
      <Input 
        inputContainerStyle = {styles.input}
        inputStyle ={styles.textInputStyle}
        containerStyle ={{marginTop: 8, marginBottom:8}}
        placeholder="Password"
        value={password}
        placeholderTextColor={'white'}
        onChangeText={setPassword}
        secureTextEntry
        leftIconContainerStyle={{ margin: 8 }}
        leftIcon={<FontAwesome name="key" size={24} color="white" />}
        onChangeText={e => setPassword(e)}
      />
      <TouchableOpacity style = {styles.loginButton} onPress={signIn} >
        <Text style={styles.loginButtonText}>Login</Text>
      </TouchableOpacity>
      </View>
    </View>
  );
}
const styles = StyleSheet.create({
  container: {
    flex:1,
    flexDirection:"column"
  },
  titleContainer: {
    flex: 0.5,
    backgroundColor: 'white',
    alignItems:'center',
    justifyContent:'center'
  },
  titleText:{
    color: '#3399ff',
    fontSize: 32,
    fontWeight: "bold"
  },
  loginFormContainer:{
    flex: 0.5,
    backgroundColor: '#3399ff',
    padding: 12
  },
  input:{
    borderColor: 'white',
    borderWidth:1,
    padding:8
  },
  textInputStyle:{
    fontSize: 16,
    color: 'white',
  },
  loginButton:{
    padding: 12,
    margin: 28,
    backgroundColor: '#3399ff',
    alignItems:'center',
    borderRadius: 12,
    borderColor:'white',
    borderWidth: 2
  },
  loginButtonText:{
    color: 'white',
    fontSize: 16,
    fontWeight:'bold'},
});


export default SignIn