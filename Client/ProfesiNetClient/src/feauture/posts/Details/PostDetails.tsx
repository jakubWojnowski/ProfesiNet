import {FC} from "react";
import {Button, Card} from "semantic-ui-react";
import {Post} from "../../../app/modules/interfaces/Post.ts";

interface Props {
    post: Post;
    cancelSelectPost: () => void;
    openForm: (id: string) => void;
    

}


const PostDetails: FC<Props> = ({post, cancelSelectPost, openForm}: Props) => {
    return (
        <Card>

            <Card.Content>
                {post.id}
            </Card.Content>
            <Card.Content extra>
                <Button.Group widths='4'>
                    <Button color='red' floated='right' content='Delete'/>
                    <Button onClick={()=> openForm(post.id)} color='blue' floated='right' content='Edit'/>
                    <Button onClick={cancelSelectPost} color='grey' floated='right' content='Cancel'/>
                </Button.Group>
            </Card.Content>
        </Card>
    );
}

export default PostDetails;