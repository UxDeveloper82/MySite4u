import React from "react";
import { Grid } from "semantic-ui-react";
import EventDetailedHeader from "./EventDetailedHeader";
import EventDetailedSidebar from "./EventDetailedSidebar";
import EventDetailedInfo from "./EventDetailedInfo";
import EventDetailedChat from "./EventDetailedChat";



export default function EventDetailedPage() {
    return(
        <Grid>
            <Grid.Column width={10}>
               <EventDetailedHeader />
               <EventDetailedInfo/>
               <EventDetailedChat />
            </Grid.Column>
            <Grid.Column with={6}>
              <EventDetailedSidebar/>                 
            </Grid.Column>
        </Grid>
    )
}