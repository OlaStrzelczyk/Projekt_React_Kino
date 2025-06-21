import React from "react";

function withLogger(WrappedComponent) {
  return function LoggedComponent(props) {
    console.log(`📋 Render komponentu: ${WrappedComponent.name}`);
    return <WrappedComponent {...props} />;
  };
}

export default withLogger;
