import {FC, useState} from "react";
import {Button, Icon, Divider, Grid, Header} from 'semantic-ui-react';
import PhotoDropZone from "../../../app/common/form/PhotoDropZone.tsx";

const ManageProfileImage: FC = () => {
    const [file, setFile] = useState<Blob | null>(null);
    return (
        <Grid>
            <Grid.Column width={16}>
             <Header sub color='teal' content='Add Photo' />
                <PhotoDropZone setFile ={setFile}/>
                <Divider />
            </Grid.Column>
  
        </Grid>
    );
};

export default ManageProfileImage;