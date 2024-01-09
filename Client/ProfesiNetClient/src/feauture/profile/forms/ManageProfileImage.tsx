import {FC, useState} from "react";
import {Button, Divider, Grid, Header, Image} from 'semantic-ui-react';
import PhotoDropZone from "../../../app/common/form/PhotoDropZone.tsx";
import {useStore} from "../../../app/stores/Store.ts";
import {observer} from "mobx-react-lite";

const ManageProfileImage: FC = () => {
    const {profileStore:{uploadProfilePhoto, uploading}, modalStore} = useStore();
    const {closeModal} = modalStore;
    
    const handleCancel = () => {
        files.forEach(file => URL.revokeObjectURL(file.preview));
        setFiles([]);
    };
    const [files, setFiles] = useState<any[]>([]);
    const handleUploadImage = () => {
        uploadProfilePhoto(files[0]).then(() => {
        setFiles([]);
        closeModal();
        });
        
    };
    return (
        <Grid>
            <Grid.Column width={16}>
             <Header sub color='teal' content='Add Photo' />

                {files && files.length > 0 ? (
                    <div>
                        <Image src={files[0].preview} />
                        <Button onClick={handleCancel}>Cancel</Button>
                        <Button onClick={handleUploadImage} loading={uploading}>Upload</Button>
                    </div>
                ) : (
                    <PhotoDropZone setFiles={setFiles} />
                )}
                <Divider />
                
                
            </Grid.Column>
  
        </Grid>
    );
};

export default observer(ManageProfileImage);