﻿using System;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Tests
{
    public class ApprovalChainTest : WorkflowManagementSystemTest
    {
        [Test]
        public void CreateApprovalChain()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();

            var approvalChainInputViewModel = new ApprovalChainInputViewModel();
            approvalChainInputViewModel.Name = "Program";
            approvalChainInputViewModel.Roles.Add(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            approvalChainInputViewModel.Roles.Add(RoleTestHelper.FACULTY_COUNCIL_MEMBER);
            approvalChainInputViewModel.Roles.Add(RoleTestHelper.APPC_MEMBER);
            approvalChainInputViewModel.Roles.Add(RoleTestHelper.GFC_MEMBER);
            approvalChainInputViewModel.Active = true;

            // act
            FacadeFactory.GetDomainFacade().CreateApprovalChain(approvalChainInputViewModel);

            // assert
            FacadeFactory.GetDomainFacade().FindAllApprovalChains().Count.ShouldBeEquivalentTo(1);

            var approvalChainSteps = FacadeFactory.GetDomainFacade().FindActiveApprovalChainSteps(approvalChainInputViewModel.Name);
            approvalChainSteps.Count.ShouldBeEquivalentTo(4);
            approvalChainSteps.Should().Contain(x => x.Sequence == 1 && x.RoleName.Equals(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER));
            approvalChainSteps.Should().Contain(x => x.Sequence == 2 && x.RoleName.Equals(RoleTestHelper.FACULTY_COUNCIL_MEMBER));
            approvalChainSteps.Should().Contain(x => x.Sequence == 3 && x.RoleName.Equals(RoleTestHelper.APPC_MEMBER));
            approvalChainSteps.Should().Contain(x => x.Sequence == 4 && x.RoleName.Equals(RoleTestHelper.GFC_MEMBER));
        }

        [Test]
        public void FindActiveApprovalChain()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();

            var approvalChainInputViewModel = new ApprovalChainInputViewModel();
            approvalChainInputViewModel.Name = "Program";
            approvalChainInputViewModel.Roles.Add(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            approvalChainInputViewModel.Roles.Add(RoleTestHelper.FACULTY_COUNCIL_MEMBER);
            approvalChainInputViewModel.Roles.Add(RoleTestHelper.APPC_MEMBER);
            approvalChainInputViewModel.Roles.Add(RoleTestHelper.GFC_MEMBER);
            approvalChainInputViewModel.Active = false;
            FacadeFactory.GetDomainFacade().CreateApprovalChain(approvalChainInputViewModel);

            var activeApprovalChain = new ApprovalChainInputViewModel();
            activeApprovalChain.Name = "Program";
            activeApprovalChain.Roles.Add(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            activeApprovalChain.Roles.Add(RoleTestHelper.FACULTY_COUNCIL_MEMBER);
            activeApprovalChain.Roles.Add(RoleTestHelper.APPC_MEMBER);
            activeApprovalChain.Active = true;
            FacadeFactory.GetDomainFacade().CreateApprovalChain(activeApprovalChain);
            FacadeFactory.GetDomainFacade().FindAllApprovalChains().Count.ShouldBeEquivalentTo(2);

            // act
            var approvalChainSteps = FacadeFactory.GetDomainFacade().FindActiveApprovalChainSteps(activeApprovalChain.Name);

            // assert
            approvalChainSteps.Count.ShouldBeEquivalentTo(3);
            approvalChainSteps.Should().Contain(x => x.Sequence == 1 && x.RoleName.Equals(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER));
            approvalChainSteps.Should().Contain(x => x.Sequence == 2 && x.RoleName.Equals(RoleTestHelper.FACULTY_COUNCIL_MEMBER));
            approvalChainSteps.Should().Contain(x => x.Sequence == 3 && x.RoleName.Equals(RoleTestHelper.APPC_MEMBER));
        }


        [Test]
        public void CreateApprovalChain_NoName()
        {
            // assemble
            var approvalChainInputViewModel = new ApprovalChainInputViewModel();

            // act
            Action act = ()=> FacadeFactory.GetDomainFacade().CreateApprovalChain(approvalChainInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Approval chain name is required.");
            FacadeFactory.GetDomainFacade().FindAllApprovalChains().Count.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void CreateApprovalChain_NoSteps()
        {
            // assemble
            var approvalChainInputViewModel = new ApprovalChainInputViewModel();
            approvalChainInputViewModel.Name = "Program";

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateApprovalChain(approvalChainInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Approval chain 'Program' does not have any steps specified.");
            FacadeFactory.GetDomainFacade().FindAllApprovalChains().Count.ShouldBeEquivalentTo(0);
        }
    }
}