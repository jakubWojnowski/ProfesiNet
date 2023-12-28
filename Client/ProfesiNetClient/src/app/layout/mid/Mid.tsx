import {FC} from "react";


import './Mid.css';

import {observer} from "mobx-react-lite";


const Mid: FC = () => {
    // const {postStore} = useStore();
    //
    //
    // useEffect(() => {
    //     postStore.loadPosts().then(() => postStore.setLoadingInitial(false));
    //
    // }, [postStore]);
    //
    //
    // if (postStore.loadingInitial) return <LoadingComponent content='Loading app...'/>;
    return (
        <h1>Mid</h1>
        // <Container fluid={true}>
        //     <PostForm
        //     />
        //     <PostDashboard/>
        // </Container>
    );
}

export default observer(Mid);