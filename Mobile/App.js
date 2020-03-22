import * as React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createStackNavigator } from "@react-navigation/stack";
import Home from "./components/Home";
import Loading from "./components/Loading";
import SignIn from "./components/SignIn";
import * as SecureStore from "expo-secure-store";
const Stack = createStackNavigator();

function MainStackNavigator() {
  const [token, setToken] = React.useState("");
  React.useEffect(() => {
    // Fetch the token from storage then navigate to our appropriate place
    const bootstrapAsync = async () => {
      let userToken;
      try {
        userToken = await SecureStore.getItemAsync("userToken");
        setToken(userToken);
        global.theToken=userToken
        
      } catch (e) {
        console.log(e);
      }
    };

    bootstrapAsync();
  }, []);

  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Home" headerMode = 'screen'>
        {!token ? (
          <Stack.Screen name="SignIn" component={SignIn} options={ {headerShown: false}} />
        ) : (
          <>
            <Stack.Screen name="The JOBS" component={Home} />
            <Stack.Screen
              name="Loading"
              component={Loading}
            />
          </>
        )}
      </Stack.Navigator>
    </NavigationContainer>
  );
}

export default MainStackNavigator;
