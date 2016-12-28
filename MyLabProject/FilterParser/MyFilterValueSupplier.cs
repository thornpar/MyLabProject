using Microsoft.OData.Core.UriParser.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.Core.UriParser.Semantic;

namespace MyLabProject.FilterParser
{
    public class MyFilterValueSupplier<TSource> : QueryNodeVisitor<TSource>
        where TSource : class
    {
        public MyFilterValueSupplier()
        {
            filterValueList = new List<FilterValue> ();
        }
        public List<FilterValue> filterValueList { get; set; }
        private FilterValue current = new FilterValue();

        public override TSource Visit(BinaryOperatorNode nodeIn)
        {
            if (nodeIn.OperatorKind == Microsoft.OData.Core.UriParser.TreeNodeKinds.BinaryOperatorKind.And
                || nodeIn.OperatorKind == Microsoft.OData.Core.UriParser.TreeNodeKinds.BinaryOperatorKind.Or)
            {
                current.LogicalOperator = nodeIn.OperatorKind.ToString();
            }
            else
            {
                current.ComparisonOperator = nodeIn.OperatorKind.ToString();
            }
            nodeIn.Right.Accept(this);
            nodeIn.Left.Accept(this);
            return current as TSource;
        }
        public override TSource Visit(SingleValuePropertyAccessNode nodeIn)
        {
            current.FieldName = nodeIn.Property.Name;
            //We are finished, add current to collection.
            filterValueList.Add(current);
            //Reset current
            current = new FilterValue();
            return current as TSource;
        }

        public override TSource Visit(ConstantNode nodeIn)
        {
            current.Value = nodeIn.LiteralText;
            return current as TSource;
        }

        public override TSource Visit(AllNode nodeIn)
        {
            FilterValue filterValue = new FilterValue { FieldName = nodeIn.Source.Kind.ToString() };
            filterValueList.Add(filterValue);
            return filterValue as TSource;
        }

        public override TSource Visit(AnyNode nodeIn)
        {
            FilterValue filterValue = new FilterValue { FieldName = nodeIn.Source.Kind.ToString() };
            filterValueList.Add(filterValue);
            return filterValue as TSource;
        }

        public override TSource Visit(CollectionFunctionCallNode nodeIn)
        {
            FilterValue filterValue = new FilterValue { FieldName = nodeIn.Name };
            filterValueList.Add(filterValue);
            return filterValue as TSource;
        }



        public override TSource Visit(CollectionNavigationNode nodeIn)
        {
            FilterValue filterValue = new FilterValue { FieldName = nodeIn.Kind.ToString() };
            filterValueList.Add(filterValue);
            return filterValue as TSource;
        }
        public override TSource Visit(CollectionPropertyAccessNode nodeIn)
        {
            FilterValue filterValue = new FilterValue { FieldName = nodeIn.Property.Name };
            filterValueList.Add(filterValue);
            return filterValue as TSource;
        }

        public override TSource Visit(ConvertNode nodeIn)
        {
            FilterValue filterValue = new FilterValue { FieldName = nodeIn.Source.TypeReference.ToString() };
            filterValueList.Add(filterValue);
            return filterValue as TSource;
        }

        public override TSource Visit(EntityCollectionCastNode nodeIn)
        {
            FilterValue filterValue = new FilterValue { FieldName = nodeIn.Kind.ToString() };
            filterValueList.Add(filterValue);
            return filterValue as TSource;
        }

        public override TSource Visit(SingleValueOpenPropertyAccessNode nodeIn)
        {
            FilterValue filterValue = new FilterValue { FieldName = nodeIn.Name };
            filterValueList.Add(filterValue);
            return filterValue as TSource;
        }

        public override TSource Visit(SingleEntityCastNode nodeIn)
        {
            FilterValue filterValue = new FilterValue { FieldName = nodeIn.Kind.ToString() };
            filterValueList.Add(filterValue);
            return filterValue as TSource;
        }
    }
}
